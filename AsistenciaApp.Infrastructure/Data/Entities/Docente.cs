using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Docente
{
    public int IdDocente { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
