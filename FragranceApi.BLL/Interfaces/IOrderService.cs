using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragranceApi.DTOs.Orders;

namespace FragranceApi.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto dto);

        Task<OrderDto?> GetByIdAsync(int id);
    }
}
