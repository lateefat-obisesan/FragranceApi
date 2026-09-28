using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FragranceApi.BLL.Interfaces;
using FragranceApi.DAL.Repositories;
using FragranceApi.DTOs.Products;
using FragranceApi.Models;

namespace FragranceApi.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync(
            string? name,
            decimal? minPrice,
            decimal? maxPrice,
            int? minStock,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize)
        {
            var products = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                products = products
                    .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (minPrice.HasValue)
            {
                products = products
                    .Where(p => p.Price >= minPrice.Value)
                    .ToList();
            }

            if (maxPrice.HasValue)
            {
                products = products
                    .Where(p => p.Price <= maxPrice.Value)
                    .ToList();
            }

            if (minStock.HasValue)
            {
                products = products
                    .Where(p => p.Stock >= minStock.Value)
                    .ToList();
            }

            if (sortBy == "price")
            {
                if (sortDescending)
                    products = products.OrderByDescending(p => p.Price).ToList();
                else
                    products = products.OrderBy(p => p.Price).ToList();
            }
            else if (sortBy == "name")
            {
                if (sortDescending)
                    products = products.OrderByDescending(p => p.Name).ToList();
                else
                    products = products.OrderBy(p => p.Name).ToList();
            }

            products = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            Product product = _mapper.Map<Product>(dto);

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
        public async Task<bool> UpdateStockAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            product.Stock = dto.Stock;

            _repository.Update(product);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
