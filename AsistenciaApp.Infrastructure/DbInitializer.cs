using AsistenciaApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaApp.Infrastructure
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Crea las tablas si no existen
            context.Database.EnsureCreated();

            if (!context.Usuarios.Any(u => u.Correo == "admin@test.com"))
            {
                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Admin",
                    Correo = "admin@test.com",
                    Contrasena = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Rol = "Director"
                });

                context.SaveChanges();
            }
        }
    }
}