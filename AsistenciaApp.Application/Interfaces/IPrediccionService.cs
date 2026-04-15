using AsistenciaApp.Application.DTOs.Prediccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IPrediccionService
    {
        Task<List<PrediccionDto>> Generar(GenerarPrediccionDto dto);
        Task<List<HistorialPrediccionDto>> ObtenerHistorialPorRiesgo(decimal minimo);
    }
}
