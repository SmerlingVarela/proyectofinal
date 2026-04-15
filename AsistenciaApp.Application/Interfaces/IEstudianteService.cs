using AsistenciaApp.Application.DTOs.Estudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IEstudianteService
    {
        Task Crear(CrearEstudianteDto dto);
        Task<List<EstudianteDto>> ObtenerPorCurso(int idCurso);
        Task Actualizar(int id, ActualizarEstudianteDto dto);
        Task Eliminar(int id);
    }
}
