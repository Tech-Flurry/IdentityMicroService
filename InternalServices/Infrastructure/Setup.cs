using DataAcess.Infrastructure;
using Domain.Infrastucture;
using InternalServices.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternalServices.Infrastructure
{
    public static class Setup
    {
        public static void UseInternalServices(this IServiceCollection services, Options options)
        {
            //initiating data access layer
            services.UseDAL();

            services.AddSingleton<IEncryptionKey>(x => new EncryptionKey(options.CryptographicKey));
            services.AddSingleton<ISaltValue>(x => new SaltValue(options.Salt));
            services.AddSingleton<ICryptography, Cryptography>();
            services.AddSingleton<ITokenHandler, TokenHandler>();
            services.AddSingleton<ISessionTimeouts>(x => new SessionTimeouts(options.ApplictionSessionTimeout, options.UserSessionTimeout));

            //Implementing Convention based dependency Injection
            services.AddTransientByConvention(Assembly.GetExecutingAssembly(), x => x.Name.EndsWith("Service"), x => x.Name.EndsWith("Service"));
        }
        public class Options
        {
            public string CryptographicKey { get; set; }
            public string Salt { get; set; }
            public int ApplictionSessionTimeout { get; set; }
            public int UserSessionTimeout { get; set; }
        }
    }
}
