using AsistenciaApp.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}
