using System;
using System.Linq;
using System.Reflection;
using OSRS.Domain.Seed;
using OSRS.Infrastructure.Repositories.Resiliency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OSRS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .LoadDomainRepositories()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddScoped<ISqlRetryPolicy, SqlRetryPolicy>()
                .AddScoped<ISystemLogger, SystemLogger>();
            return services;
        }
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper((requestServices, action) =>
            {
                action.ConstructServicesUsing(requestServices.GetService);
            }, new[] { Assembly.GetExecutingAssembly(), typeof(OperationResult).Assembly, typeof(DbLoggerCategory.Infrastructure).Assembly }, ServiceLifetime.Scoped);

        }
        
        private static IServiceCollection LoadDomainRepositories(this IServiceCollection services)
        {
            var infrastructureAssembly = typeof(DependencyInjection).Assembly;
            var domainAssembly = typeof(IDomainRepository).Assembly;
            
            foreach (var i in domainAssembly.GetTypes().Where(p => p.IsPublic && p.GetInterface(nameof(IDomainRepository)) != null))
            {
                foreach (var j in infrastructureAssembly.GetTypes().Where(p => p.IsPublic && p.IsClass && p.GetInterface(i.Name) != null))
                {
                    AddCommonImplementationService(services, i, j);
                }
            }

            return services;
        }
        
        public static IServiceCollection AddCommonImplementationService(this IServiceCollection services,
            Type service, Type implementation)
        {
            ServiceDescriptor descriptor;
            if ((descriptor = services.FirstOrDefault(p => p.ImplementationType == implementation)) != null)
            {
                if (descriptor.ServiceType != service)
                    services.AddScoped(service, p 
                        => p.GetRequiredService(descriptor.ServiceType));
            }
            else
                services.AddScoped(service, implementation);
            return services;
        }
    }
}
