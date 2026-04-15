using AsistenciaApp.Application.DTOs.TutorEstudiante;
using AsistenciaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstudianteTutorController : ControllerBase
    {
        private readonly IEstudianteTutorService _service;

        public EstudianteTutorController(IEstudianteTutorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(AsignarTutorDto dto)
        {
            await _service.Asignar(dto);
            return Ok("Tutor asignado al estudiante");
        }

        [HttpDelete("{idEstudiante}/{idTutor}")]
        public async Task<IActionResult> Desasignar(int idEstudiante, int idTutor)
        {
            await _service.Desasignar(new DesasignarTutorDto
            {
                IdEstudiante = idEstudiante,
                IdTutor = idTutor
            });

            return Ok("Tutor desasignado correctamente");
        }
    }
}
