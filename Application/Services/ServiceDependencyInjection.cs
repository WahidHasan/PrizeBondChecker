using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PrizeBondChecker.Services
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            //services.AddHostedService<SchedulerService>();
            services.AddTransient<IPrizebondService, PrizebondService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
