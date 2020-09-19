using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Domain.Infrastucture
{
    public static class Extentions
    {
        /// <summary>
        /// Converts a string to first word as uppercase other small
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns></returns>
        public static string ToInitialCapital(this string s)
        {
            string firstLetterCapital = s.Substring(0, 1).ToUpper();
            string restWordSmall = s.Substring(1).ToLower();
            return firstLetterCapital + restWordSmall;
        }
        public static IServiceCollection AddSingletonsByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> interfacePredicate, Func<Type, bool> implementationPredicate)
        {
            var interfaces = assembly.ExportedTypes
                .Where(x => x.IsInterface && interfacePredicate(x))
                .ToList();
            var implementations = assembly.ExportedTypes
                .Where(x => !x.IsInterface && !x.IsAbstract && implementationPredicate(x))
                .ToList();
            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(x => @interface.IsAssignableFrom(x));
                if (implementation == null) continue;
                services.AddSingleton(@interface, implementation);
            }
            return services;
        }

        public static IServiceCollection AddSingletonsByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> predicate)
        {
            return services.AddSingletonsByConvention(assembly, predicate, predicate);
        }

        public static IServiceCollection AddScopesByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> interfacePredicate, Func<Type, bool> implementationPredicate)
        {
            var interfaces = assembly.ExportedTypes
                .Where(x => x.IsInterface && interfacePredicate(x))
                .ToList();
            var implementations = assembly.ExportedTypes
                .Where(x => !x.IsInterface && !x.IsAbstract && implementationPredicate(x))
                .ToList();
            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(x => @interface.IsAssignableFrom(x));
                if (implementation == null) continue;
                services.AddScoped(@interface, implementation);
            }
            return services;
        }

        public static IServiceCollection AddScopesByConvention(this IServiceCollection services,
            Assembly assembly,
            Func<Type, bool> predicate)
        {
            return services.AddSingletonsByConvention(assembly, predicate, predicate);
        }

        public static IServiceCollection AddTransientByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> interfacePredicate, Func<Type, bool> implementationPredicate)
        {
            var interfaces = assembly.ExportedTypes
                .Where(x => x.IsInterface && interfacePredicate(x))
                .ToList();
            var implementations = assembly.ExportedTypes
                .Where(x => !x.IsInterface && !x.IsAbstract && implementationPredicate(x))
                .ToList();
            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(x => @interface.IsAssignableFrom(x));
                if (implementation == null) continue;
                services.AddScoped(@interface, implementation);
            }
            return services;
        }

        public static IServiceCollection AddTransientByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> predicate)
        {
            return services.AddSingletonsByConvention(assembly, predicate, predicate);
        }
    }
}
