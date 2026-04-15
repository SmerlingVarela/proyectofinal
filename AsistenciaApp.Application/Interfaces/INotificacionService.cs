using AsistenciaApp.Application.DTOs.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface INotificacionService
    {
        Task Enviar(EnviarNotificacionDto dto);
        Task NotificarRiesgoAlto(int idEstudiante, decimal porcentaje);
        Task<List<NotificacionDto>> Historial();
    }
}
