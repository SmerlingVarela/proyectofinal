using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class EstudianteTutor
{
    public int IdEstudianteTutor { get; set; }

    public int IdEstudiante { get; set; }

    public int IdTutor { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Tutor IdTutorNavigation { get; set; } = null!;
}
