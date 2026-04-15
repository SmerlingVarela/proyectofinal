using AsistenciaApp.Application.DTOs.Grado;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
    

    public class CrearGradoValidator : AbstractValidator<CrearGradoDto>
    {
        private readonly string[] _gradosValidos =
            {
                "1ro", "2do", "3ro", "4to", "5to", "6to",
                "1ro de secundaria", "2do de secundaria", "3ro de secundaria",
                "Preprimario", "Kinder"
            };
        public CrearGradoValidator()
        {
            RuleFor(x => x.Nombre)
                 .NotEmpty().WithMessage("El nombre del grado es obligatorio")
                 .Must(nombre => _gradosValidos.Contains(nombre))
                 .WithMessage("El grado ingresado no es válido. Ejemplos: 1ro, 1ro de secundaria, Preprimario.");
        }
    }
}
