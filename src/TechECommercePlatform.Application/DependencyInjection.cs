using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TechECommercePlatform.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Validators (if using FluentValidation)
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Pipeline behaviors (later)
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        
        return services;
    }
}