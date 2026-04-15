using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Services
{
    using AsistenciaApp.Application.DTOs.Asistencia;
    using AsistenciaApp.Application.Interfaces;
    using AsistenciaApp.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AsistenciaService : IAsistenciaService
    {
        private readonly AppDbContext _context;

        public AsistenciaService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 REGISTRAR ASISTENCIA
        public async Task Registrar(RegistrarAsistenciaDto dto)
        {
            // 🔍 Validar curso
            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.IdCurso == dto.IdCurso && c.Activo);

            if (curso == null)
                throw new Exception("El curso no existe o está inactivo");

            // 🔍 Validar duplicado (mismo curso + fecha)
            var yaExiste = await _context.Asistencia
                .AnyAsync(a => a.Fecha == dto.Fecha &&
                               a.IdEstudianteNavigation.IdCurso == dto.IdCurso);

            if (yaExiste)
                throw new Exception("La asistencia ya fue registrada para este curso en esa fecha");

            // 🔍 Obtener estudiantes válidos del curso
            var estudiantesCurso = await _context.Estudiantes
                .Where(e => e.IdCurso == dto.IdCurso && e.Activo)
                .Select(e => e.IdEstudiante)
                .ToListAsync();

            var lista = new List<Asistencium>();

            foreach (var item in dto.Estudiantes)
            {
                // 🔥 Validar que el estudiante pertenezca al curso
                if (!estudiantesCurso.Contains(item.IdEstudiante))
                    throw new Exception($"El estudiante {item.IdEstudiante} no pertenece al curso");

                lista.Add(new Asistencium
                {
                    IdEstudiante = item.IdEstudiante,
                    Fecha = dto.Fecha,
                    Estado = item.Estado.ToString() // 🔥 ENUM → STRING
                });
            }

            await _context.Asistencia.AddRangeAsync(lista);
            await _context.SaveChangesAsync();
        }

        // 🔹 OBTENER ASISTENCIA
        public async Task<List<AsistenciaDto>> ObtenerPorCursoYFecha(int idCurso, DateOnly fecha)
        {
            return await _context.Asistencia
                .Where(a => a.Fecha == fecha &&
                            a.IdEstudianteNavigation.IdCurso == idCurso)
                .Include(a => a.IdEstudianteNavigation)
                .Select(a => new AsistenciaDto
                {
                    IdEstudiante = a.IdEstudiante,
                    Nombre = a.IdEstudianteNavigation.Nombre + " " +
                             a.IdEstudianteNavigation.Apellido,
                    Estado = a.Estado,
                    Fecha = a.Fecha
                })
                .ToListAsync();
        }
    }
}
