using AsistenciaApp.Application.DTOs.TutorEstudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IEstudianteTutorService
    {
        Task Asignar(AsignarTutorDto dto);
        Task Desasignar(DesasignarTutorDto dto);
    }
}
