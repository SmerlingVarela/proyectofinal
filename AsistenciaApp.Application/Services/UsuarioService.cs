using AsistenciaApp.Application.DTOs.Usuario;
using AsistenciaApp.Application.Interfaces;
using AsistenciaApp.Domain.Enum;
using AsistenciaApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AsistenciaApp.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDto>> ObtenerTodos()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.IdUsuario,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Rol = u.Rol
                }).ToListAsync();
        }

        public async Task<UsuarioDto> Crear(CrearUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena),
                Rol = dto.Rol.ToString()
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            
            if (dto.Rol == Roles.Docente)
            {
                var docente = new Docente
                {
                    IdUsuario = usuario.IdUsuario
                };

                _context.Docentes.Add(docente);
                await _context.SaveChangesAsync();
            }

            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };


        }

        public async Task Actualizar(int id, ActualizarUsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.Nombre = dto.Nombre;
            usuario.Rol = dto.Rol;

            await _context.SaveChangesAsync();
        }


        public async Task Eliminar(int id, string correoActual)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            // 🔐 Evitar auto eliminación
            if (usuario.Correo == correoActual)
                throw new Exception("No puedes eliminarte a ti mismo");

            // 🔥 Eliminación lógica
            usuario.Activo = false;

            // 🔥 Si es docente → desactivar también
            var docente = await _context.Docentes
                .FirstOrDefaultAsync(d => d.IdUsuario == usuario.IdUsuario);

            if (docente != null)
            {
                // opcional si luego agregas Activo a Docente
                // docente.Activo = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
