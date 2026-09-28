using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragranceApi.DTOs.Products;

namespace FragranceApi.BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync(
            string? name,
            decimal? minPrice,
            decimal? maxPrice,
            int? minStock,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize);

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task<bool> UpdateStockAsync(int id, UpdateProductDto dto);
    }
}
