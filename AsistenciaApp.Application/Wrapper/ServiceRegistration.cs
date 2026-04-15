using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using AsistenciaApp.Application.Services; // Ajusta a tu namespace real
using AsistenciaApp.Application.Interfaces; // Ajusta a tu namespace real

namespace AsistenciaApp.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // 1. Registro de Validadores de FluentValidation
        // Usamos Assembly.GetExecutingAssembly() para que busque en este proyecto (Application)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 2. Registro de Servicios de Lógica de Negocio
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IGradoService, GradoService>();
        services.AddScoped<ISeccionService, SeccionService>();
        services.AddScoped<ICursoService, CursoService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEstudianteService, EstudianteService>();
        services.AddScoped<IAsistenciaService, AsistenciaService>();
        services.AddScoped<IPrediccionService, PrediccionService>();
        services.AddScoped<ITutorService, TutorService>();
        services.AddScoped<IEstudianteTutorService, EstudianteTutorService>();
        services.AddScoped<INotificacionService, NotificacionService>();



        return services;
    }
}