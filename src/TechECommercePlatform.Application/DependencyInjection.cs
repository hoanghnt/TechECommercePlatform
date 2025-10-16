using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TechECommercePlatform.Application.Common.Behaviours;

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
        
        // Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Behaviours.ValidationBehavior<,>));
        
        return services;
    }
}