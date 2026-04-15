using AsistenciaApp.Application.DTOs.Curso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface ICursoService
    {
        Task Crear(CrearCursoDto dto);
        Task<List<CursoDto>> ObtenerTodos();

        Task<List<CursoDto>> ObtenerPorCorreoDocente(string correo);

        Task Actualizar(int id, ActualizarCursoDto dto);

        Task Eliminar(int id);

    }
}
