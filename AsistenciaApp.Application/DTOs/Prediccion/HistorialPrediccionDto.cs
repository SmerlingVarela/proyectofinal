using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Prediccion
{
    public class HistorialPrediccionDto
    {
        public string Estudiante { get; set; }
        public decimal Probabilidad { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
