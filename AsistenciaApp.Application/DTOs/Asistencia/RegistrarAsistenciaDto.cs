using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Asistencia
{
    public class RegistrarAsistenciaDto
    {
        public int IdCurso { get; set; }
        public DateOnly Fecha { get; set; }
        public List<AsistenciaItemDto> Estudiantes { get; set; }
    }
}
