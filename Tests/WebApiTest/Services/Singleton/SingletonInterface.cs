using FluentDI;

namespace WebApiTest.Services.Singleton
{
    [Injector<ISingletonInterface>(ServiceLifetime.Singleton)]
    public class SingletonInterface : ISingletonInterface
    {
        public string Value { get; set; }

        public SingletonInterface()
        {
            Value = "This could change since it's singleton and instance is kept";
        }

        public string Get()
        {
            return Value;
        }
    }
}