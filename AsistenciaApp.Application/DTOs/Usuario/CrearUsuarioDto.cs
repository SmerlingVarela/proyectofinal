using AsistenciaApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Usuario
{
    public class CrearUsuarioDto
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public Roles Rol { get; set; }
    }
}
