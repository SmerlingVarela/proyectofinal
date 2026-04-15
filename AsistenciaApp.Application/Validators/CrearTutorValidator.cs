using AsistenciaApp.Application.DTOs.Tutor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    public class CrearTutorValidator : AbstractValidator<CrearTutorDto>
    {
        public CrearTutorValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.TelefonoWhatsapp)
                .NotEmpty().WithMessage("El teléfono es obligatorio")
                .Matches(@"^\+?[0-9]{8,15}$")
                .WithMessage("El teléfono no es válido");

            RuleFor(x => x.Parentesco)
                .NotEmpty().WithMessage("El parentesco es obligatorio")
                .MaximumLength(50);
        }
    }
}
