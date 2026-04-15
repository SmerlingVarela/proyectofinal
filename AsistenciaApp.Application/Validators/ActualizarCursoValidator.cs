using AsistenciaApp.Application.DTOs.Curso;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    public class ActualizarCursoValidator : AbstractValidator<ActualizarCursoDto>
    {
        public ActualizarCursoValidator()
        {
            RuleFor(x => x.IdDocente)
                .GreaterThan(0);

            RuleFor(x => x.AnioEscolar)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
