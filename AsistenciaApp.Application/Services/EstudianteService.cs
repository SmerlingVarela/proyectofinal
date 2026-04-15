using AsistenciaApp.Application.DTOs.Estudiante;
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
    public class EstudianteService : IEstudianteService
    {
        private readonly AppDbContext _context;

        public EstudianteService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 CREAR
        public async Task Crear(CrearEstudianteDto dto)
        {
            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.IdCurso == dto.IdCurso && c.Activo);

            if (curso == null)
                throw new Exception("El curso no existe o está inactivo");

            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                IdCurso = dto.IdCurso,
                Activo = true
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
        }

        // 🔹 OBTENER POR CURSO (🔥 BIEN DISEÑADO)
        public async Task<List<EstudianteDto>> ObtenerPorCurso(int idCurso)
        {
            return await _context.Estudiantes
                .Where(e => e.IdCurso == idCurso) // QueryFilter ya aplica Activo
                .Select(e => new EstudianteDto
                {
                    IdEstudiante = e.IdEstudiante,
                    NombreCompleto = e.Nombre + " " + e.Apellido,
                    FechaNacimiento = e.FechaNacimiento,
                    Curso = e.IdCursoNavigation.IdGradoNavigation.Nombre
                             + " - " + e.IdCursoNavigation.IdSeccionNavigation.Nombre
                })
                .ToListAsync();
        }

        // 🔹 ACTUALIZAR
        public async Task Actualizar(int id, ActualizarEstudianteDto dto)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdEstudiante == id);

            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

            if (!estudiante.Activo)
                throw new Exception("No se puede modificar un estudiante inactivo");

            estudiante.Nombre = dto.Nombre;
            estudiante.Apellido = dto.Apellido;

            await _context.SaveChangesAsync();
        }

        // 🔹 ELIMINAR (LÓGICO 🔥)
        public async Task Eliminar(int id)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdEstudiante == id);

            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

            if (!estudiante.Activo)
                throw new Exception("El estudiante ya está inactivo");

            estudiante.Activo = false;

            await _context.SaveChangesAsync();
        }
    }
}
