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

            if(!await context.Products.AnyAsync())
            {
                context.Products.AddRange(
                     new Product
                     {
                         Name = "Vanilla Glow Candle",
                         Description = "A warm vanilla scented candle.",
                         Price = 43.99m,
                         Stock = 20
                     },
                    new Product
                    {
                        Name = "Lavender Mist Candle",
                        Description = "A relaxing lavender scented candle.",
                        Price = 38.99m,
                        Stock = 15
                    },
                    new Product
                    {
                        Name = "Citrus Bloom Candle",
                        Description = "A fresh citrus scented candle.",
                        Price = 27.99m,
                        Stock = 25
                    }
                );
            }
            await context.SaveChangesAsync();
        }
    }
}
