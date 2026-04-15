using AsistenciaApp.Application.DTOs.Curso;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _service;

        public CursoController(ICursoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearCursoDto dto)
        {
            await _service.Crear(dto);
            return Ok("Curso creado correctamente");
        }

        [HttpGet]
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTodos());
        }


        [HttpGet("mis-cursos")]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> MisCursos()
        {
            var correo = User.FindFirst(ClaimTypes.Email)?.Value;

            var cursos = await _service.ObtenerPorCorreoDocente(correo);

            return Ok(cursos);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Actualizar(int id, ActualizarCursoDto dto)
        {
            await _service.Actualizar(id, dto);
            return Ok("Curso actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);
            return Ok("Curso desactivado");
        }
    }
}
