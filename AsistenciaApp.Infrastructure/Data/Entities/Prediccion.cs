using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Prediccion
{
    public int IdPrediccion { get; set; }

    public int IdEstudiante { get; set; }

    public decimal ProbabilidadAbandono { get; set; }

    public DateOnly FechaPrediccion { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;
}
