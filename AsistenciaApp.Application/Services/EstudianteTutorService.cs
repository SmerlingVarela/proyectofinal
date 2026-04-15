using AsistenciaApp.Application.DTOs.TutorEstudiante;
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
    public class EstudianteTutorService : IEstudianteTutorService
    {
        private readonly AppDbContext _context;

        public EstudianteTutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Asignar(AsignarTutorDto dto)
        {
            var existe = await _context.EstudianteTutors
                .AnyAsync(et => et.IdEstudiante == dto.IdEstudiante && et.IdTutor == dto.IdTutor);

            if (existe)
                throw new Exception("Ya existe esta relación");

            var relacion = new EstudianteTutor
            {
                IdEstudiante = dto.IdEstudiante,
                IdTutor = dto.IdTutor
            };

            _context.EstudianteTutors.Add(relacion);
            await _context.SaveChangesAsync();
        }

        public async Task Desasignar(DesasignarTutorDto dto)
        {
            var relacion = await _context.EstudianteTutors
                .FirstOrDefaultAsync(et =>
                    et.IdEstudiante == dto.IdEstudiante &&
                    et.IdTutor == dto.IdTutor);

            if (relacion == null)
                throw new Exception("Relación no encontrada");

            _context.EstudianteTutors.Remove(relacion);

            await _context.SaveChangesAsync();
        }
    }
}
