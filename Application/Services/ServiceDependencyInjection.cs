using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PrizeBondChecker.Services
{
    public static class ServiceDependencyInjection
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
