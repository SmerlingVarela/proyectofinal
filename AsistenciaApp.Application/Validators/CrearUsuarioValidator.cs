using AsistenciaApp.Application.DTOs.Usuario;
using AsistenciaApp.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    public class CrearUsuarioValidator : AbstractValidator<CrearUsuarioDto>
    {
        public CrearUsuarioValidator()
        {
            RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio")
                .EmailAddress().WithMessage("El formato del correo no es válido");
            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(20).WithMessage("La contraseña no puede superar los 20 caracteres.");
            RuleFor(x => x.Rol)
                .IsInEnum()
                .Must(r => r == Roles.Director || r == Roles.Docente)
                .WithMessage("El rol debe ser Director o Docente.");


        }
    }
}
