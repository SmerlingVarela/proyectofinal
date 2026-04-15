using AsistenciaApp.Application.DTOs.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface ITutorService
    {
        Task Crear(CrearTutorDto dto);
        Task<List<TutorDto>> ObtenerTodos();
        Task Actualizar(int id, ActualizarTutorDto dto);
        Task Eliminar(int id);
    }
}
