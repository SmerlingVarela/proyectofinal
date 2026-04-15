using System;
using System.Collections.Generic;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class Tutor
{
    public int IdTutor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string TelefonoWhatsapp { get; set; } = null!;

    public string Parentesco { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<EstudianteTutor> EstudianteTutors { get; set; } = new List<EstudianteTutor>();

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
