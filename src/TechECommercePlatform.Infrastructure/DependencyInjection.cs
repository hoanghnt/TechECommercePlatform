using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechECommercePlatform.Application.Common.Interfaces;
using TechECommercePlatform.Infrastructure.Persistence;
using TechECommercePlatform.Infrastructure.Persistence.Interceptors;
using TechECommercePlatform.Infrastructure.Services;

namespace TechECommercePlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register interceptors
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();
        
        // DbContext
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var auditableEntityInterceptor = sp.GetService<AuditableEntitySaveChangesInterceptor>()!;
            var dispatchDomainEventsInterceptor = sp.GetService<DispatchDomainEventsInterceptor>()!;
       
            options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => 
                        builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .AddInterceptors(auditableEntityInterceptor, dispatchDomainEventsInterceptor);
        });

        // Register IApplicationDbContext
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Services
        services.AddTransient<IDateTime, DateTimeService>();

        // More infrastructure services as needed

        return services;
    }
}