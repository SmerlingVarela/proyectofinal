
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
