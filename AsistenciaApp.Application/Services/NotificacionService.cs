using AsistenciaApp.Application.DTOs.Notificacion;
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
    public class NotificacionService : INotificacionService
    {
        private readonly AppDbContext _context;

        public NotificacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Enviar(EnviarNotificacionDto dto)
        {
            var tutores = await _context.EstudianteTutors
                .Where(et => et.IdEstudiante == dto.IdEstudiante)
                .Include(et => et.IdTutorNavigation)
                .ToListAsync();

            if (!tutores.Any())
                throw new Exception("El estudiante no tiene tutor asignado");

            foreach (var item in tutores)
            {
                var tutor = item.IdTutorNavigation;

                // 🔥 Guardar notificación
                var notificacion = new Notificacion
                {
                    IdEstudiante = dto.IdEstudiante,
                    IdTutor = tutor.IdTutor,
                    Mensaje = dto.Mensaje,
                    FechaEnvio = DateTime.Now,
                    Estado = "Pendiente"
                };

                _context.Notificacions.Add(notificacion);

                // 🔥 AQUÍ LUEGO VAS A INTEGRAR WHATSAPP API
            }

            await _context.SaveChangesAsync();
        }

        public async Task NotificarRiesgoAlto(int idEstudiante, decimal porcentaje)
        {
            var tutores = await _context.EstudianteTutors
                .Where(et => et.IdEstudiante == idEstudiante)
                .Include(et => et.IdTutorNavigation)
                .ToListAsync();

            if (!tutores.Any())
                return; // no rompas el flujo

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);

            foreach (var item in tutores)
            {
                var tutor = item.IdTutorNavigation;

                var mensaje = $"⚠️ El estudiante {estudiante.Nombre} {estudiante.Apellido} presenta un {porcentaje}% de ausencias. Riesgo alto de abandono.";

                var notificacion = new Notificacion
                {
                    IdEstudiante = idEstudiante,
                    IdTutor = tutor.IdTutor,
                    Mensaje = mensaje,
                    FechaEnvio = DateTime.Now,
                    Estado = "Enviado"
                };

                _context.Notificacions.Add(notificacion);

                // 🔥 Aquí luego integras WhatsApp
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<NotificacionDto>> Historial()
        {
            return await _context.Notificacions
                .Include(n => n.IdEstudianteNavigation)
                .Include(n => n.IdTutorNavigation)
                .OrderByDescending(n => n.FechaEnvio)
                .Select(n => new NotificacionDto
                {
                    Estudiante = n.IdEstudianteNavigation.Nombre + " " +
                                 n.IdEstudianteNavigation.Apellido,
                    Tutor = n.IdTutorNavigation.Nombre + " " +
                            n.IdTutorNavigation.Apellido,
                    Mensaje = n.Mensaje,
                    Fecha = n.FechaEnvio
                })
                .ToListAsync();
        }
    }
}
