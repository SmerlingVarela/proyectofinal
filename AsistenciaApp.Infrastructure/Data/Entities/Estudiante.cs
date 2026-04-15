using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int IdCurso { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<EstudianteTutor> EstudianteTutors { get; set; } = new List<EstudianteTutor>();

    public virtual Curso IdCursoNavigation { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Prediccion> Prediccions { get; set; } = new List<Prediccion>();
}
