using AsistenciaApp.Application.DTOs.Grado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IGradoService
    {
        Task Crear(CrearGradoDto dto);
        Task<List<object>> ObtenerTodos();
        Task Actualizar(int id, ActualizarGradoDto dto);

        Task Eliminar(int id);
    }
}
