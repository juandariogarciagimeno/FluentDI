using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<IScopedInterface>]
    public class ScopedInterface : IScopedInterface
    {
        public string Value { get; set; }

        public ScopedInterface()
        {
            Value = "This value should always be the same since it's scoped";
        }

        public string Get()
        {
            return Value;
        }
    }
}