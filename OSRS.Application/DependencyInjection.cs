using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSRS.Domain.Seed;

namespace OSRS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapperConfiguration();
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