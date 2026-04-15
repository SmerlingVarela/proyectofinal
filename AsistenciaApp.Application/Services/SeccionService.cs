using AsistenciaApp.Application.DTOs.Seccion;
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
    public class SeccionService : ISeccionService
    {
        private readonly AppDbContext _context;

        public SeccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Crear(CrearSeccionDto dto)
        {
            var seccion = new Seccion
            {
                Nombre = dto.Nombre,
                IdGrado = dto.IdGrado
            };

            _context.Seccions.Add(seccion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> ObtenerPorGrado(int idGrado)
        {
            return await _context.Seccions
                .Where(s => s.IdGrado == idGrado)
                .Select(s => new
                {
                    s.IdSeccion,
                    s.Nombre
                }).ToListAsync<object>();
        }

        public async Task Actualizar(int id, ActualizarSeccionDto dto)
        {
            var seccion = await _context.Seccions.FindAsync(id);

            if (seccion == null)
                throw new Exception("Sección no encontrada");

            if (!seccion.Activo)
                throw new Exception("No se puede modificar una sección inactiva");

            seccion.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var seccion = await _context.Seccions
                .Include(s => s.Cursos)
                .ThenInclude(c => c.Estudiantes)
                .FirstOrDefaultAsync(s => s.IdSeccion == id);

            if (seccion == null)
                throw new Exception("Sección no encontrada");

            if (!seccion.Activo)
                throw new Exception("La sección ya está inactiva");

            seccion.Activo = false;

            foreach (var curso in seccion.Cursos)
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
