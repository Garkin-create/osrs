using OSRS.Application.Seed.Pipeline;
using OSRS.Domain;
using OSRS.Domain.Seed;
using FluentValidation.AspNetCore;

namespace OSRS.Application
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapperConfiguration();
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(new[] { assembly }));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));
            return services;
        }
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper((requestServices, action) =>
            {
                action.ConstructServicesUsing(requestServices.GetService);
            }, new[] { Assembly.GetExecutingAssembly(), typeof(OperationResult).Assembly, typeof(DbLoggerCategory.Infrastructure).Assembly }, ServiceLifetime.Scoped);

        }
    }
}
