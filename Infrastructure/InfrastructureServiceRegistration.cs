using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Infrastructure.auth;
using Infrastructure.DBContext;
using Infrastructure.Repository.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PrizeBondChecker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IDbContext), typeof(DbContext));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthService, AuthService>();
            services.Configure<DbConnectionDetails>(configuration.GetSection("DbConnectionDetails"));
            return services;
        }

        public static IServiceCollection AddMongoIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("DbConnectionDetails").Get<MongoDbSettings>();
            services.AddSingleton<MongoDbSettings>(settings);

            services.AddIdentityCore<ApplicationUser>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                           configuration["DbConnectionDetails:ConnectionString"],
                           configuration["DbConnectionDetails:DatabaseName"]
                       );

            services.AddHttpContextAccessor();
            // Identity services
            //services.TryAddScoped<IUserValidator<ApplicationUser>, UserValidator<ApplicationUser>>();
            //services.TryAddScoped<IPasswordValidator<ApplicationUser>, PasswordValidator<ApplicationUser>>();
            //services.TryAddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            //services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            //services.TryAddScoped<IRoleValidator<ApplicationRole>, RoleValidator<ApplicationRole>>();

            //services.TryAddScoped<IdentityErrorDescriber>();
            //services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();
            //services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<ApplicationUser>>();
            //services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

            services.TryAddScoped<UserManager<ApplicationUser>>();
            services.TryAddScoped<SignInManager<ApplicationUser>>();
            services.TryAddScoped<RoleManager<ApplicationRole>>();
            return services;
        }
    }
}
