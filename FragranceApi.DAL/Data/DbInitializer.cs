using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragranceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FragranceApi.DAL.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(FragranceDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Sade Johnson",
                        Email = "sade@example.com",
                        Phone = "204-555-1001"
                    },
                    new Customer
                    {
                        Name = "Keji Williams",
                        Email = "keji@example.com",
                        Phone = "204-555-1002"
                    }
                );
            }
        }
    }
}
