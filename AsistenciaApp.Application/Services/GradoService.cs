using AsistenciaApp.Application.DTOs.Grado;
using AsistenciaApp.Application.Interfaces;
using AsistenciaApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Services
{
    public class GradoService : IGradoService
    {
        private readonly AppDbContext _context;

        public GradoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Crear(CrearGradoDto dto)
        {
            var existe = await _context.Grados.AnyAsync(g => g.Nombre == dto.Nombre);

            if (existe)
                throw new Exception("El grado ya existe");
            var grado = new Grado
            {
                Nombre = dto.Nombre
            };

            _context.Grados.Add(grado);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> ObtenerTodos()
        {
            return await _context.Grados
                .Select(g => new
                {
                    g.IdGrado,
                    g.Nombre
                }).ToListAsync<object>();
        }

        public async Task Actualizar(int id, ActualizarGradoDto dto)
        {
            var grado = await _context.Grados.FindAsync(id);

            if (grado == null)
                throw new Exception("Grado no encontrado");

            if (!grado.Activo)
                throw new Exception("No se puede modificar un grado inactivo");

            grado.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var grado = await _context.Grados
                .Include(g => g.Cursos)
                .ThenInclude(c => c.Estudiantes)
                .FirstOrDefaultAsync(g => g.IdGrado == id);

            if (grado == null)
                throw new Exception("Grado no encontrado");

            if (!grado.Activo)
                throw new Exception("El grado ya está inactivo");

            // 🔥 Desactivar grado
            grado.Activo = false;

            // 🔥 Cascada lógica
            foreach (var curso in grado.Cursos)
            {
                curso.Activo = false;

                foreach (var estudiante in curso.Estudiantes)
                {
                    estudiante.Activo = false;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
