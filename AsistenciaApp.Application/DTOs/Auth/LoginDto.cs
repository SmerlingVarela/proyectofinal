using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
