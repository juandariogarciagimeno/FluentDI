using FluentDI;

namespace WebApiTest.Services.Singleton
{
    [Injector<SingletonNoInterface>(ServiceLifetime.Singleton)]
    public class SingletonNoInterface
    {
        public string Value { get; set; }

        public SingletonNoInterface() 
        {
            Value = "This could change since it's singleton and instance is kept";
        }

        public string Get()
        {
            return Value;
        }
    }
}
