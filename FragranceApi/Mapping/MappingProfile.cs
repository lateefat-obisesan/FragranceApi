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

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(
                    destination => destination.ProductName,
                    options => options.MapFrom(source => source.Product.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(
                    destination => destination.CustomerName,
                    options => options.MapFrom(source => source.Customer.Name))
                .ForMember(
                    destination => destination.Items,
                    options => options.MapFrom(source => source.OrderItems));
        }
    }
}
