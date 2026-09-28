using AutoMapper;
using FragranceApi.DTOs.Customers;
using FragranceApi.DTOs.Orders;
using FragranceApi.DTOs.Products;
using FragranceApi.Models;

namespace FragranceApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
        }
    }
}
