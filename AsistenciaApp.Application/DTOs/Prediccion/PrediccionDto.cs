using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Prediccion
{
    public class PrediccionDto
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public decimal ProbabilidadAbandono { get; set; }
        public string NivelRiesgo { get; set; }
    }
}
