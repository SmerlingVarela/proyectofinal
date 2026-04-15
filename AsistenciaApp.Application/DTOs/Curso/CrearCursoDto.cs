using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Curso
{
    public class CrearCursoDto
    {
        public int IdGrado { get; set; }
        public int IdSeccion { get; set; }
        public int IdDocente { get; set; }
        public string AnioEscolar { get; set; }
    }
}
