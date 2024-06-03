using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<ScopedNoInterface>]
    public class ScopedNoInterface
    {
        public string Value { get; set; }

        public ScopedNoInterface() 
        {
            Value = "This value should always be the same since it's scoped";
        }

        public string Get()
        {
            return Value;
        }
    }
}
