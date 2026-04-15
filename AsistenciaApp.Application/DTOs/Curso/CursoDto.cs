using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Curso
{
    public class CursoDto
    {
        public int IdCurso { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public string Docente { get; set; }
        public string AnioEscolar { get; set; }
    }
}
