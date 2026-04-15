using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Estudiante
{
    public class EstudianteDto
    {
        public int IdEstudiante { get; set; }
        public string NombreCompleto { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Curso { get; set; }
    }
}
