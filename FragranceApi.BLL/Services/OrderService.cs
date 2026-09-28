using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FragranceApi.BLL.Interfaces;
using FragranceApi.DAL.Repositories;
using FragranceApi.DTOs.Orders;
using FragranceApi.Models;

namespace FragranceApi.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            Order order = new Order
            {
                CustomerId = dto.CustomerId,
                Date = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new KeyNotFoundException(
                        $"Product with ID {item.ProductId} was not found.");

                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for {product.Name}.");

                OrderItem orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                order.OrderItems.Add(orderItem);

                product.Stock -= item.Quantity;

                _productRepository.Update(product);
            }

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            OrderDto orderDto = _mapper.Map<OrderDto>(order);

            orderDto.Total = order.OrderItems
                .Sum(item => item.Price * item.Quantity);

            return orderDto;
        }
    }
}
