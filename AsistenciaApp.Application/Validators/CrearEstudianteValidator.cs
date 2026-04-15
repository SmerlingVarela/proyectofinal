using AsistenciaApp.Application.DTOs.Estudiante;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaApp.Application.Validators
{
   
    public class CrearEstudianteValidator : AbstractValidator<CrearEstudianteDto>
    {
        public CrearEstudianteValidator()
        {
            // 1. Nombre y Apellido: Sin números y con límite de largo
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El apellido solo puede contener letras.");

            
            // 3. Curso: Selección obligatoria
            RuleFor(x => x.IdCurso)
                .NotEmpty().WithMessage("Debe asignar el estudiante a un curso.")
                .GreaterThan(0).WithMessage("El curso seleccionado no es válido.");

            // Configuración para que no amontone errores: 
            // Si el nombre está vacío, que no me diga también que "solo debe tener letras"
            RuleLevelCascadeMode = CascadeMode.Stop;
        }
    }
}
