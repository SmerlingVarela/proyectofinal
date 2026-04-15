using AsistenciaApp.Application.DTOs.Curso;
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
    public class CursoService : ICursoService
    {
        private readonly AppDbContext _context;

        public CursoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Crear(CrearCursoDto dto)
        {
            // 🔥 Validar que existan
            var gradoExiste = await _context.Grados.AnyAsync(g => g.IdGrado == dto.IdGrado);
            var seccionExiste = await _context.Seccions.AnyAsync(s => s.IdSeccion == dto.IdSeccion);
            var docenteExiste = await _context.Docentes.AnyAsync(d => d.IdDocente == dto.IdDocente);

            if (!gradoExiste)
                throw new Exception("El grado no existe");

            if (!seccionExiste)
                throw new Exception("La sección no existe");

            if (!docenteExiste)
                throw new Exception("El docente no existe");

            // 🔥 Validar que la sección pertenezca al grado
            var seccionValida = await _context.Seccions
                .AnyAsync(s => s.IdSeccion == dto.IdSeccion && s.IdGrado == dto.IdGrado);

            if (!seccionValida)
                throw new Exception("La sección no pertenece al grado");

            // 🔥 Evitar duplicados
            var existeCurso = await _context.Cursos.AnyAsync(c =>
                c.IdGrado == dto.IdGrado &&
                c.IdSeccion == dto.IdSeccion &&
                c.AnioEscolar == dto.AnioEscolar);

            if (existeCurso)
                throw new Exception("Ya existe este curso para ese año");

            var curso = new Curso
            {
                IdGrado = dto.IdGrado,
                IdSeccion = dto.IdSeccion,
                IdDocente = dto.IdDocente,
                AnioEscolar = dto.AnioEscolar
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CursoDto>> ObtenerTodos()
        {
            return await _context.Cursos
                .Include(c => c.IdGradoNavigation)
                .Include(c => c.IdSeccionNavigation)
                .Include(c => c.IdDocenteNavigation)
                    .ThenInclude(d => d.IdUsuarioNavigation)
                .Select(c => new CursoDto
                {
                    IdCurso = c.IdCurso,
                    Grado = c.IdGradoNavigation.Nombre,
                    Seccion = c.IdSeccionNavigation.Nombre,
                    Docente = c.IdDocenteNavigation.IdUsuarioNavigation.Nombre,
                    AnioEscolar = c.AnioEscolar
                })
                .ToListAsync();
        }

        public async Task<List<CursoDto>> ObtenerPorCorreoDocente(string correo)
        {
            // 🔥 Buscar docente por correo (desde Usuario)
            var docente = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(d => d.IdUsuarioNavigation.Correo == correo);

            if (docente == null)
                throw new Exception("Docente no encontrado");

            return await _context.Cursos
                .Where(c => c.IdDocente == docente.IdDocente)
                .Include(c => c.IdGradoNavigation)
                .Include(c => c.IdSeccionNavigation)
                .Include(c => c.IdDocenteNavigation)
                    .ThenInclude(d => d.IdUsuarioNavigation)
                .Select(c => new CursoDto
                {
                    IdCurso = c.IdCurso,
                    Grado = c.IdGradoNavigation.Nombre,
                    Seccion = c.IdSeccionNavigation.Nombre,
                    Docente = c.IdDocenteNavigation.IdUsuarioNavigation.Nombre,
                    AnioEscolar = c.AnioEscolar
                })
                .ToListAsync();
        }

        public async Task Actualizar(int id, ActualizarCursoDto dto)
        {
            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.IdCurso == id);

            if (curso == null)
                throw new Exception("Curso no encontrado");

            if (!curso.Activo)
                throw new Exception("No se puede modificar un curso inactivo");

            // 🔍 Validar docente
            var docenteExiste = await _context.Docentes
                .AnyAsync(d => d.IdDocente == dto.IdDocente);

            if (!docenteExiste)
                throw new Exception("El docente no existe");

            // 🔍 Evitar duplicados
            var existe = await _context.Cursos.AnyAsync(c =>
                c.IdGrado == curso.IdGrado &&
                c.IdSeccion == curso.IdSeccion &&
                c.AnioEscolar == dto.AnioEscolar &&
                c.IdCurso != id);

            if (existe)
                throw new Exception("Ya existe un curso con ese grado, sección y año escolar");

            curso.IdDocente = dto.IdDocente;
            curso.AnioEscolar = dto.AnioEscolar;

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Estudiantes)
                .FirstOrDefaultAsync(c => c.IdCurso == id);

            if (curso == null)
                throw new Exception("Curso no encontrado");

            if (!curso.Activo)
                throw new Exception("El curso ya está inactivo");

            curso.Activo = false;

            foreach (var estudiante in curso.Estudiantes)
            {
                estudiante.Activo = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
