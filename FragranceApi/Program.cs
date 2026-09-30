using FragranceApi.BLL.Interfaces;
using FragranceApi.BLL.Services;
using FragranceApi.DAL.Data;
using FragranceApi.DAL.Repositories;
using FragranceApi.Mapping;
using FragranceApi.Middleware;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FragranceApi.Validators;

namespace FragranceApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
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

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FragranceDbContext>();

                await db.Database.MigrateAsync();

                await DbInitializer.SeedAsync(db);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            //app.MapControllers();
            app.MapGet("/", () => "Fragrance API is running");

            app.Run();
        }
    }
}