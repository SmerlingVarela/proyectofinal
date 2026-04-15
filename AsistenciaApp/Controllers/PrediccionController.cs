using AsistenciaApp.Application.DTOs.Prediccion;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PrediccionController : ControllerBase
    {
        private readonly IPrediccionService _service;

        public PrediccionController(IPrediccionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Generar(GenerarPrediccionDto dto)
        {
            var result = await _service.Generar(dto);
            return Ok(result);
        }

        [HttpGet("historial")]
        public async Task<IActionResult> Historial(decimal minimo = 60)
        {
            var data = await _service.ObtenerHistorialPorRiesgo(minimo);
            return Ok(data);
        }
     }
}
