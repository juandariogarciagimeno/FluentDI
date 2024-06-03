using FluentDI;

namespace WebApiTest.Services.Scoped
{
    public class ScopeSelector : IInjectorSelector<IScopedInterface>
    {
        public bool CanInject(IScopedInterface instance)
        {
            return instance.GetType() == typeof(ScopedInterface2);
        }
    }
}
