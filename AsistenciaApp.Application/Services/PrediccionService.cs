using AsistenciaApp.Application.DTOs.Prediccion;
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
    public class PrediccionService : IPrediccionService
    {
        private readonly AppDbContext _context;

        private readonly INotificacionService _notificacionService;

        public PrediccionService(AppDbContext context, INotificacionService notificacionService)
        {
            _context = context;
            _notificacionService = notificacionService;
        }

        public async Task<List<PrediccionDto>> Generar(GenerarPrediccionDto dto)
        {
            // 🔍 Obtener estudiantes del curso
            var estudiantes = await _context.Estudiantes
                .Where(e => e.IdCurso == dto.IdCurso && e.Activo)
                .Include(e => e.Asistencia)
                .ToListAsync();

            var resultado = new List<PrediccionDto>();

            foreach (var estudiante in estudiantes)
            {
                var asistencias = estudiante.Asistencia
                    .Where(a => a.Fecha >= dto.FechaInicio && a.Fecha <= dto.FechaFin)
                    .ToList();

                if (!asistencias.Any())
                    continue;

                var total = asistencias.Count;
                var ausencias = asistencias.Count(a => a.Estado == "Ausente");

                // 🔥 CÁLCULO
                decimal porcentajeAusencia = (decimal)ausencias / total * 100;

                string nivel;

                if (porcentajeAusencia < 20)
                {
                    nivel = "Bajo";
                }
                else if (porcentajeAusencia < 50)
                {
                    nivel = "Medio";
                }
                else
                {
                    nivel = "Alto";

                    // 🔥 VALIDAR SI YA SE NOTIFICÓ HOY
                    var yaNotificado = await _context.Notificacions
                        .AnyAsync(n =>
                            n.IdEstudiante == estudiante.IdEstudiante &&
                            n.FechaEnvio.Date == DateTime.Now.Date &&
                            n.Mensaje.Contains("Riesgo alto"));

                    // 🔥 SOLO NOTIFICAR SI NO EXISTE
                    if (!yaNotificado)
                    {
                        await _notificacionService.NotificarRiesgoAlto(
                            estudiante.IdEstudiante,
                            porcentajeAusencia
                        );
                    }
                }

                var prediccion = new Prediccion
                {
                    IdEstudiante = estudiante.IdEstudiante,
                    ProbabilidadAbandono = porcentajeAusencia,
                    FechaPrediccion = DateOnly.FromDateTime(DateTime.Now)
                };

                var existeHoy = await _context.Prediccions
                       .AnyAsync(p =>
                           p.IdEstudiante == estudiante.IdEstudiante &&
                           p.FechaPrediccion == DateOnly.FromDateTime(DateTime.Now));

                if (!existeHoy)
                {
                    _context.Prediccions.Add(prediccion);
                }

                resultado.Add(new PrediccionDto
                {
                    IdEstudiante = estudiante.IdEstudiante,
                    Nombre = estudiante.Nombre + " " + estudiante.Apellido,
                    ProbabilidadAbandono = porcentajeAusencia,
                    NivelRiesgo = nivel
                });
            }

            return resultado;
        }

        public async Task<List<HistorialPrediccionDto>> ObtenerHistorialPorRiesgo(decimal minimo)
        {
            return await _context.Prediccions
                .Where(p => p.ProbabilidadAbandono >= minimo)
                .Include(p => p.IdEstudianteNavigation)
                .OrderByDescending(p => p.FechaPrediccion)
                .Select(p => new HistorialPrediccionDto
                {
                    Estudiante = p.IdEstudianteNavigation.Nombre + " " +
                                 p.IdEstudianteNavigation.Apellido,
                    Probabilidad = p.ProbabilidadAbandono,
                    Fecha = p.FechaPrediccion
                })
                .ToListAsync();
        }
    }
}
