using AsistenciaApp.Application;
using AsistenciaApp.Application.Interfaces;
using AsistenciaApp.Application.Services;
using AsistenciaApp.Extensions;
using AsistenciaApp.Infrastructure;
using AsistenciaApp.Infrastructure.Data.Entities;
using AsistenciaApp.Infrastructure.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DbInitializer.Seed(context);
            }

            if (app.Environment.IsDevelopment())
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