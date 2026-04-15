using AsistenciaApp.Application.DTOs.Tutor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    public class ActualizarTutorValidator : AbstractValidator<ActualizarTutorDto>
    {
        public ActualizarTutorValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TelefonoWhatsapp)
                .NotEmpty()
                .Matches(@"^\+?[0-9]{8,15}$");
            RuleFor(x => x.Parentesco).NotEmpty().MaximumLength(50);
        }
    }
}
