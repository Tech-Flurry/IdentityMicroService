using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            var implementations = assembly.GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && implementationPredicate(x))
                .ToList();
            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(x => @interface.IsAssignableFrom(x));
                if (implementation == null) continue;
                services.AddTransient(@interface, implementation);
            }
            return services;
        }

        public static IServiceCollection AddTransientByConvention(this IServiceCollection services, Assembly assembly, Func<Type, bool> predicate)
        {
            return services.AddSingletonsByConvention(assembly, predicate, predicate);
        }

        public static string ToJson(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }
        public static object ToObject(this string json, Type type)
        {
            var obj = JsonConvert.DeserializeObject(json, type);
            return obj;
        }
        public static T ToObject<T>(this string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
        /// <summary>
        /// Converts a List or array to a datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                Type type;
                if (prop.PropertyType == typeof(object))
                {
                    type = typeof(string);
                }
                else
                {
                    type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                }
                //Defining type of data column gives proper data table 
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
