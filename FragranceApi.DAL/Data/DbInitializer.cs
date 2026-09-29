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

            }
        }
    }
}
