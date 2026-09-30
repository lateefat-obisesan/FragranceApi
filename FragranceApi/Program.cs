using FragranceApi.BLL.Interfaces;
using FragranceApi.BLL.Services;
using FragranceApi.DAL.Data;
using FragranceApi.DAL.Repositories;
using FragranceApi.Mapping;
using FragranceApi.Middleware;
using FragranceApi.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FragranceApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

            builder.Services.AddDbContext<FragranceDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository Registrations
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            // Service Registrations
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Register global exception handling right after building the app
            app.UseMiddleware<ExceptionMiddleware>();

            // Database migration and seeding
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FragranceDbContext>();

                await db.Database.MigrateAsync();
                await DbInitializer.SeedAsync(db);
            }

            // Configure HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}