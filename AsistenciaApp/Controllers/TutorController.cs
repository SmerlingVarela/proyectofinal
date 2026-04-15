using AsistenciaApp.Application.DTOs.Tutor;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;

        public TutorController(ITutorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearTutorDto dto)
        {
            await _service.Crear(dto);
            return Ok("Tutor creado");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTodos());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarTutorDto dto)
        {
            await _service.Actualizar(id, dto);
            return Ok("Tutor actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);
            return Ok("Tutor eliminado");
        }
    }
}
