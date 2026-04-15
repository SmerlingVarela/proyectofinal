using AsistenciaApp.Application.DTOs.Notificacion;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _service;

        public NotificacionController(INotificacionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(EnviarNotificacionDto dto)
        {
            await _service.Enviar(dto);
            return Ok("Notificación enviada");
        }

        [HttpGet("historial")]
        public async Task<IActionResult> Historial()
        {
            return Ok(await _service.Historial());
        }
    }
}
