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
            builder.Services.AddOpenApi();

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
