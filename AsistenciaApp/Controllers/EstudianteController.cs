using AsistenciaApp.Application.DTOs.Estudiante;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EstudianteController : ControllerBase
{
    private readonly IEstudianteService _service;

    public EstudianteController(IEstudianteService service)
    {
        _service = service;
    }

    // 🔹 CREAR
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearEstudianteDto dto)
    {
        await _service.Crear(dto);
        return Ok("Estudiante creado correctamente");
    }

    // 🔹 OBTENER POR CURSO (🔥 TU CASO PRINCIPAL)
    [HttpGet("curso/{idCurso}")]
    public async Task<IActionResult> ObtenerPorCurso(int idCurso)
    {
        var estudiantes = await _service.ObtenerPorCurso(idCurso);
        return Ok(estudiantes);
    }

    // 🔹 ACTUALIZAR
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarEstudianteDto dto)
    {
        await _service.Actualizar(id, dto);
        return Ok("Estudiante actualizado");
    }

    // 🔹 ELIMINAR (LÓGICO)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _service.Eliminar(id);
        return Ok("Estudiante desactivado");
    }
}
