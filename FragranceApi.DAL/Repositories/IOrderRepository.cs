using FragranceApi.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragranceApi.DAL.Data;
using FragranceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FragranceApi.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}
