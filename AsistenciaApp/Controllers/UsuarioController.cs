using AsistenciaApp.Application.DTOs.Usuario;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AsistenciaApp.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.ObtenerTodos();
            return Ok(usuarios);
        }
        [Authorize(Roles = "Director")]
        [HttpPost]
        public async Task<IActionResult> Crear(CrearUsuarioDto dto)
        {
            var usuario = await _service.Crear(dto);
            return Ok(usuario);
        }

        [Authorize(Roles = "Director")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarUsuarioDto dto)
        {
            await _service.Actualizar(id, dto);
            return Ok("Usuario actualizado");
        }

        [Authorize(Roles = "Director")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var correo = User.FindFirst(ClaimTypes.Email)?.Value;

            await _service.Eliminar(id, correo);

            return Ok("Usuario desactivado");
        }
    }
}
