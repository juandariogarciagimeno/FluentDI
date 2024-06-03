using FluentDI;

namespace WebApiTest.Services.Transient
{
    [Injector<TransientNoInterface>(ServiceLifetime.Transient)]
    public class TransientNoInterface
    {
        public string Value { get; set; }

        public TransientNoInterface() 
        {
            Value = "This value should always be the same since it's transient";
        }

        public string Get()
        {
            return Value;
        }
    }
}
