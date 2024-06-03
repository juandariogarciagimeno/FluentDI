using Microsoft.Extensions.DependencyInjection;

namespace FluentDI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Injector<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; } = lifetime;
    }
}
