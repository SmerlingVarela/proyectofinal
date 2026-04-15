using AsistenciaApp.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AsistenciaApp.Infrastructure
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
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
