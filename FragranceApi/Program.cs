using FragranceApi.BLL.Interfaces;
using FragranceApi.BLL.Services;
using FragranceApi.DAL.Data;
using FragranceApi.DAL.Repositories;
using FragranceApi.Mapping;
using FragranceApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace FragranceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddDbContext<FragranceDbContext>(options =>
                 options.UseSqlServer(
                     builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile)); //add to do (Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection) for you to work.


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
