using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Curso
{
    public int IdCurso { get; set; }

    public int IdGrado { get; set; }

    public int IdSeccion { get; set; }

    public int IdDocente { get; set; }

    public string AnioEscolar { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Grado IdGradoNavigation { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;
}
