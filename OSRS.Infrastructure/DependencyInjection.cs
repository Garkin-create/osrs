using System;
using System.Linq;
using System.Reflection;
using OSRS.Domain.Seed;
using OSRS.Infrastructure.Repositories.Resiliency;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSRS.Domain.Entities;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Domain.Service;
using OSRS.Infrastructure.ApiClient;
using OSRS.Infrastructure.Model.Domain;
using OSRS.Infrastructure.Repositories;
using OSRS.Infrastructure.Services;
using Stu.Cubatel.Infrastructure.Model.Domain;

namespace OSRS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
           
            services
                .AddIdentityCore<IdentityUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DomainContext>();
            services
                .LoadDomainRepositories()
                // .AddMediatR(Assembly.GetExecutingAssembly())
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

        public static IServiceCollection AddAutenticationService(this IServiceCollection services)
        {
            services
                .AddIdentityCore<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DomainContext>();
           
            // services.AddSingleton<IdentityUserContext<IdentityUser>, DomainContext>();

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
            
            services.AddScoped<IDomainUnitOfWork, DomainUnitOfWork>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IKeywordRepository, KeywordRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            
            //SERVICES
            services.AddScoped<IWordPressService, WordPressService>();
            services.AddScoped<IRestApiClient, RestApiClient>();
            services.AddScoped<ISearchGoogleService, SearchGoogleService>();
            services.AddScoped<JwtService>();
            
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
