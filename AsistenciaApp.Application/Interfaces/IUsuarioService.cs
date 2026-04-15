using AsistenciaApp.Application.DTOs.Usuario;
using AsistenciaApp.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IUsuarioService
    {

        Task<UsuarioDto> Crear(CrearUsuarioDto dto);
        Task<List<UsuarioDto>> ObtenerTodos();
        Task Actualizar(int id, ActualizarUsuarioDto dto);
        Task Eliminar(int id, string correoActual);
    }
}
