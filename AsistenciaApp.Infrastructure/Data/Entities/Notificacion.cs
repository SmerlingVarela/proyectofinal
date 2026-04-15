using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Notificacion
{
    public int IdNotificacion { get; set; }

    public int IdEstudiante { get; set; }

    public int IdTutor { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    public string Estado { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Tutor IdTutorNavigation { get; set; } = null!;
}
