using AsistenciaApp.Application.DTOs.Seccion;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    public class CrearSeccionValidator : AbstractValidator<CrearSeccionDto>
    {
        public CrearSeccionValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la sección es obligatorio")
                .Matches(@"^[a-zA-Z]$").WithMessage("La sección debe ser una única letra (A-Z)");

            RuleFor(x => x.IdGrado)
                .GreaterThan(0).WithMessage("Debe seleccionar un grado válido");
        }
    }
}
