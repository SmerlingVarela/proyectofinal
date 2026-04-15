using AsistenciaApp.Application.DTOs.Grado;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GradoController : ControllerBase
    {
        private readonly IGradoService _service;

        public GradoController(IGradoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearGradoDto dto)
        {
            await _service.Crear(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTodos());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarGradoDto dto)
        {
            await _service.Actualizar(id, dto);
            return Ok("Grado actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);
            return Ok("Grado desactivado");
        }
    }
}
