using AsistenciaApp.Application.DTOs.Tutor;
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
    public class TutorService : ITutorService
    {
        private readonly AppDbContext _context;

        public TutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Crear(CrearTutorDto dto)
        {
            var tutor = new Tutor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                TelefonoWhatsapp = dto.TelefonoWhatsapp,
                Parentesco = dto.Parentesco,
                Activo = true
            };

            _context.Tutors.Add(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TutorDto>> ObtenerTodos()
        {
            return await _context.Tutors
                .Select(t => new TutorDto
                {
                    IdTutor = t.IdTutor,
                    NombreCompleto = t.Nombre + " " + t.Apellido,
                    TelefonoWhatsapp = t.TelefonoWhatsapp,
                    Parentesco = t.Parentesco
                })
                .ToListAsync();
        }

        public async Task Actualizar(int id, ActualizarTutorDto dto)
        {
            var tutor = await _context.Tutors.FindAsync(id);

            if (tutor == null)
                throw new Exception("Tutor no encontrado");

            tutor.Nombre = dto.Nombre;
            tutor.Apellido = dto.Apellido;
            tutor.TelefonoWhatsapp = dto.TelefonoWhatsapp;
            tutor.Parentesco = dto.Parentesco;

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var tutor = await _context.Tutors.FindAsync(id);

            if (tutor == null)
                throw new Exception("Tutor no encontrado");

            tutor.Activo = false; // 🔥 BORRADO LÓGICO

            await _context.SaveChangesAsync();
        }
    }
}
