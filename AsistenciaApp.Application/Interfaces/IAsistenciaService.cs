using AsistenciaApp.Application.DTOs.Asistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IAsistenciaService
    {
        Task Registrar(RegistrarAsistenciaDto dto);
        Task<List<AsistenciaDto>> ObtenerPorCursoYFecha(int idCurso, DateOnly fecha);
    }
}
