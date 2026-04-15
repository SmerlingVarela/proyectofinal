using AsistenciaApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Asistencia
{
    public class AsistenciaItemDto
    {
        public int IdEstudiante { get; set; }
        public EstadoAsistencia Estado { get; set; }
    }
}
