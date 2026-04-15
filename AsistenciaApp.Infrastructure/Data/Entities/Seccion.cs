using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdGrado { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual Grado IdGradoNavigation { get; set; } = null!;
}
