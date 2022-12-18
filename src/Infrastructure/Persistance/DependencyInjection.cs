using System;
using System.Linq;
using OSRS.Domain;
using OSRS.Domain.IRepositories;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Persistance.Helper;
using OSRS.Persistance.Model.Domain;
using OSRS.Persistance.Repositories;
using OSRS.Persistance.Services;
using OperationResult = OSRS.Domain.Seed.OperationResult;

namespace OSRS.Persistance
{
    using OSRS.Common.General;
    using OSRS.Persistance.Db;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
            AddAutoMapperConfiguration(services);
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            #region "Repositories"
            services
                .LoadDomainRepositories();
            #endregion
            services.AddSingleton<ISystemLogger, SystemLogger>();
            services.AddScoped((serviceProvider) =>
            {
                var option = CreateContextOptions(appOptions.ReadDatabaseConnectionString);
                return new OSRSReadOnlyDbContext(option);
            });

            services.AddScoped((serviceProvider) =>
            {
                var option = CreateContextOptions(appOptions.WriteDatabaseConnectionString);
                return new OSRSWriteDbContext(option);
            });

            DbContextOptions<AppDbContext> CreateContextOptions(string connectionString)
            {
                var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                                     .UseSqlServer(connectionString)
                                     .Options;

                return contextOptions;
            }

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(appOptions.WriteDatabaseConnectionString));

            services.AddScoped<IAppDbContext, AppDbContext>();
            //services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
            return services;
        }
        
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper((requestServices, action) =>
            {
                action.ConstructServicesUsing(requestServices.GetService);
            }, new[] { Assembly.GetExecutingAssembly(), typeof(OperationResult).Assembly, typeof(DbLoggerCategory.Infrastructure).Assembly }, ServiceLifetime.Scoped);

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
    }
}
