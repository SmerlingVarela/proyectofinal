using AsistenciaApp.Application.DTOs.Seccion;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SeccionController : ControllerBase
    {
        private readonly ISeccionService _service;

        public SeccionController(ISeccionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearSeccionDto dto)
        {
            await _service.Crear(dto);
            return Ok();
        }

        [HttpGet("grado/{idGrado}")]
        public async Task<IActionResult> Get(int idGrado)
        {
            return Ok(await _service.ObtenerPorGrado(idGrado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarSeccionDto dto)
        {
            await _service.Actualizar(id, dto);
            return Ok("Sección actualizada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);
            return Ok("Sección desactivada");
        }
    }
}
