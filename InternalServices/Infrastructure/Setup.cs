using DataAcess.Infrastructure;
using Domain.Infrastucture;
using Domain.Infrastucture.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternalServices.Infrastructure
{
    public static class Setup
    {
        public static void UseInternalServices(this IServiceCollection services)
        {
            services.UseDAL();
            services.AddTransientByConvention(Assembly.GetExecutingAssembly(), x => x.Name.EndsWith("Service"));
        }
    }
}
