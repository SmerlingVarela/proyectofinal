using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Asistencia
{
    public class AsistenciaDto
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
