using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Notificacion
{
    public class NotificacionDto
    {
        public string Estudiante { get; set; }
        public string Tutor { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
