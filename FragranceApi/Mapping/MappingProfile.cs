using FragranceApi.DAL;
using FragranceApi.DTOs.Customers;
using FragranceApi.DTOs.Orders;
using FragranceApi.DTOs.Products;

namespace FragranceApi.Mapping
{
    public class MappingProfile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();

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
                    options => options.MapFrom(source => source.OrderItems))
                .ForMember(
                    destination => destination.Total,
                    options => options.MapFrom(
                        source => source.OrderItems.Sum(
                            item => item.Price * item.Quantity)));
        }
    }
}
