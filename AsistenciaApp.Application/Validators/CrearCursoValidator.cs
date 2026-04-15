using AsistenciaApp.Application.DTOs.Curso;
using FluentValidation;

public class CrearCursoValidator : AbstractValidator<CrearCursoDto>
{
    public CrearCursoValidator()
    {
        // 1. Validar IDs (Asumiendo que son obligatorios)
        RuleFor(x => x.IdGrado)
            .NotEmpty().WithMessage("Debe seleccionar un grado.")
            .GreaterThan(0).WithMessage("El ID del grado debe ser un número válido.");

        RuleFor(x => x.IdSeccion)
            .NotEmpty().WithMessage("Debe seleccionar una sección.")
            .GreaterThan(0).WithMessage("El ID de la sección debe ser un número válido.");

        RuleFor(x => x.IdDocente)
            .NotEmpty().WithMessage("Debe asignar un docente al curso.")
            .GreaterThan(0).WithMessage("El ID del docente debe ser un número válido.");

        // 2. Validar Año Escolar (Formato y lógica)
        RuleFor(x => x.AnioEscolar)
            .NotEmpty().WithMessage("El año escolar es obligatorio.")
            .MaximumLength(10).WithMessage("El año escolar no puede exceder los 10 caracteres.")
            // Opcional: Validar formato tipo 2023-2024 o 2024
            .Matches(@"^\d{4}(-\d{4})?$").WithMessage("Formato de año escolar inválido (Ej: 2024 o 2024-2025).");

      
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}