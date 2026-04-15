using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Asistencium
{
    public int IdAsistencia { get; set; }

    public int IdEstudiante { get; set; }

    public DateOnly Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;
}
