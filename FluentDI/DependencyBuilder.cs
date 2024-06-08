using FluentDI.Factory;
using FluentDI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace FluentDI
{
    public static class DependencyBuilder
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

            var completeList = new List<TypeHolder>();

            var parentless = typesToInject.Where(x => x.Attr.GetType().GetGenericArguments()?.FirstOrDefault() == x.Type).ToList();
            completeList.AddRange(parentless);

            var typesByAcestor = typesToInject.Except(parentless).GroupBy(x => x.Attr.GetType().GetGenericArguments()?.FirstOrDefault());

            var singleImplementation = typesByAcestor.Where(x => x.Count() == 1)?.Select(x => x.FirstOrDefault())?.ToList();
            if (singleImplementation != null) 
                completeList.AddRange(singleImplementation);

            typesByAcestor = typesByAcestor.Where(x => !singleImplementation.Contains(x.FirstOrDefault()));

            //var fistOfGroup = typesByAcestor.Select(x =>
            //    x.Where(y =>
            //    {
            //        try
            //        {
            //            var selectorGeneric = typeof(ISetupDependencySelector<>).MakeGenericType(x.Key);
            //            var selector = types.FirstOrDefault(z => z.IsAssignableTo(selectorGeneric));

            //            if (selector == null) return false;

            //            var instance = Activator.CreateInstance(y.Type);

            //            var selectorInstance = Activator.CreateInstance(selector);
            //            var canInject = (bool)(selectorInstance?.GetType().GetMethod("CanInject")?.Invoke(selectorInstance, [instance]) ?? true);

            //            return canInject;
            //        }
            //        catch
            //        { return true; }
            //    })
            //).FirstOrDefault();

            //completeList.AddRange(fistOfGroup);

            //typesByAcestor = typesByAcestor.Where(x => fistOfGroup != null && x.Any(y => !fistOfGroup.Contains(y)));

            List<ServiceDescriptor> services = new List<ServiceDescriptor>();

            foreach (var ms in typesByAcestor)
            {
                var selectorGeneric = typeof(IRuntimeDependencySelector<>).MakeGenericType(ms.Key);
                var selector = types.FirstOrDefault(z => z.IsAssignableTo(selectorGeneric));
                var factory = typeof(DependencyFactory<>).MakeGenericType(ms.Key);

                if (selector == null || factory == null) continue;

                foreach (var ser in ms)
                {
                    try
                    {
                        services.Add(Parse(ser));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                ServiceDescriptor s = new ServiceDescriptor(selectorGeneric, selector, ServiceLifetime.Transient);
                services.Add(s);

                ServiceDescriptor f = new ServiceDescriptor(factory, factory, ServiceLifetime.Transient);
                services.Add(f);
            }


            foreach (var type in completeList)
            {
                try
                {
                    services.Add(Parse(type));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return services;
        }

        private static ServiceDescriptor Parse(TypeHolder type)
        {
            if (type.Attr == null) return null;

            if (type.Attr.GetType().GetGenericArguments()[0] != null && !type.Attr.GetType().GetGenericArguments()[0].IsAssignableFrom(type.Type)) return null;

            Type parent = type.Attr.GetType().GetGenericArguments()[0] ?? type.Type;
            ServiceLifetime lifetime = ((ServiceLifetime?)type.Attr.GetType().GetProperty("ServiceLifetime")?.GetValue(type.Attr)) ?? ServiceLifetime.Scoped;
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(parent, type.Type, lifetime);

            return serviceDescriptor;
        }

        private class TypeHolder
        {
            public Type Type { get; set; } = null!;
            public Attribute? Attr { get; set; }
        }
    }
}
