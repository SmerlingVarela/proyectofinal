using AsistenciaApp.Application.DTOs.Seccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface ISeccionService
    {
        Task Crear(CrearSeccionDto dto);
        Task<List<object>> ObtenerPorGrado(int idGrado);

        Task Actualizar(int id, ActualizarSeccionDto dto);
        Task Eliminar(int id);

    }
}
