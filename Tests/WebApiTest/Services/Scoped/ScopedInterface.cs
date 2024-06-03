using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<IScopedInterface>]
    public class ScopedInterface2 : IScopedInterface
    {
        public string Value { get; set; }

        public ScopedInterface2()
        {
            Value = "This value should always be the same since it's scoped";
        }

        public string Get()
        {
            return Value;
        }
    }
}