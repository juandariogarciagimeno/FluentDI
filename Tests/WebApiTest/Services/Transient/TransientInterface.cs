using FluentDI;

namespace WebApiTest.Services.Transient
{
    [Injector<ITransientInterface>(ServiceLifetime.Transient)]
    public class TransientInterface : ITransientInterface
    {
        public string Value { get; set; }

        public TransientInterface()
        {
            Value = "This value should always be the same since it's transient";
        }

        public string Get()
        {
            return Value;
        }
    }
}