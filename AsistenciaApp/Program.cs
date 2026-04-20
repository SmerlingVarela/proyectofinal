using AsistenciaApp.Application;
using AsistenciaApp.Extensions;
using AsistenciaApp.Infrastructure;
using AsistenciaApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddPresentationLayer(builder.Configuration);
            builder.Services.AddInfrastructureLayer(builder.Configuration);
            builder.Services.AddApplicationLayer();

            var app = builder.Build();

            // Crear tablas y seed
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    db.Database.EnsureCreated();
                    DbInitializer.Seed(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DB init error: " + ex.Message);
                }
            }

            if (app.Environment.IsDevelopment() || true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}