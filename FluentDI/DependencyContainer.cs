using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace FluentDI
{
    public static class DependencyContainer
    { 

        public static IHostApplicationBuilder AddFluentDI(this IHostApplicationBuilder builder)
        {
            builder.ConfigureContainer(new DefaultServiceProviderFactory(), (c) =>
            {
                GetServices().ForEach(c.Add);
            });
            
            return builder;
        }

        public static IHostBuilder AddFluentDI(this IHostBuilder builder)
        {
            builder.ConfigureServices((h,c) =>
            {
                GetServices().ForEach(c.Add);
            });

            return builder;
        }

        private static List<ServiceDescriptor> GetServices()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (assemblies == null || assemblies.Length == 0) return [];

            var types = assemblies.SelectMany(a => a.GetTypes());
            var typesToInject = types.Select(x =>
            {
                var attrtype = typeof(Injector<>);
                return new TypeHolder { Type = x, Attr = x.GetCustomAttribute(attrtype) };
            }).Where(x => x.Attr != null).ToList();

            if (typesToInject == null || typesToInject.Count == 0) return [];

            List<ServiceDescriptor> services = new List<ServiceDescriptor>();

            var completeList = new List<TypeHolder>();

            var parentless = typesToInject.Where(x => x.Attr.GetType().GetGenericArguments()?.FirstOrDefault() == x.Type).ToList();
            completeList.AddRange(parentless);

            var typesByAcestor = typesToInject.Except(parentless).GroupBy(x => x.Attr.GetType().GetGenericArguments()?.FirstOrDefault());

            var singleImplementation = typesByAcestor.Where(x => x.Count() == 1)?.Select(x => x.FirstOrDefault())?.ToList();
            if (singleImplementation != null) 
                completeList.AddRange(singleImplementation);

            typesByAcestor = typesByAcestor.Where(x => !singleImplementation.Contains(x.FirstOrDefault()));

            var fistOfGroup = typesByAcestor.Select(x =>
                x.Where(y =>
                {
                    try
                    {
                        var selectorGeneric = typeof(IInjectorSelector<>).MakeGenericType(x.Key);
                        var selector = types.FirstOrDefault(z => z.IsAssignableTo(selectorGeneric));

                        if (selector == null) return true;

                        var instance = Activator.CreateInstance(y.Type);

                        var selectorInstance = Activator.CreateInstance(selector);
                        var canInject = (bool)(selectorInstance?.GetType().GetMethod("CanInject")?.Invoke(selectorInstance, [instance]) ?? true);

                        return canInject;
                    }
                    catch
                    { return true; }
                })
            ).FirstOrDefault();

            completeList.AddRange(fistOfGroup);

            foreach (var type in completeList)
            {
                try
                {
                    if (type.Attr == null) continue;

                    if (type.Attr.GetType().GetGenericArguments()[0] != null && !type.Attr.GetType().GetGenericArguments()[0].IsAssignableFrom(type.Type)) continue;

                    Type parent = type.Attr.GetType().GetGenericArguments()[0] ?? type.Type;
                    ServiceLifetime lifetime = ((ServiceLifetime?)type.Attr.GetType().GetProperty("ServiceLifetime")?.GetValue(type.Attr)) ?? ServiceLifetime.Scoped;
                    ServiceDescriptor serviceDescriptor = new ServiceDescriptor(parent, type.Type, lifetime);
                    services.Add(serviceDescriptor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return services;
        }

        private class TypeHolder
        {
            public Type Type { get; set; } = null!;
            public Attribute? Attr { get; set; }
        }
    }
}
