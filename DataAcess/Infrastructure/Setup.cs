using Domain.Infrastucture;
using Domain.Infrastucture.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataAcess.Infrastructure
{
    public static class Setup
    {
        public static void SetupDb(this IServiceCollection services, IDbConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IDbConnection, DbConnection>();
        }
        public static void UseDAL(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddTransientByConvention(assembly, x => x.Name.EndsWith("Repository"), x => x.Name.EndsWith("Repository"));
        }
    }
}
