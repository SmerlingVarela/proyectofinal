using AsistenciaApp.Application.DTOs.Asistencia;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Docente")]
public class AsistenciaController : ControllerBase
{
    private readonly IAsistenciaService _service;

    public AsistenciaController(IAsistenciaService service)
    {
        _service = service;
    }

    // 🔹 REGISTRAR
    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarAsistenciaDto dto)
    {
        await _service.Registrar(dto);
        return Ok("Asistencia registrada correctamente");
    }

    // 🔹 CONSULTAR
    [HttpGet]
    public async Task<IActionResult> Obtener(int idCurso, DateOnly fecha)
    {
        var data = await _service.ObtenerPorCursoYFecha(idCurso, fecha);
        return Ok(data);
    }
}