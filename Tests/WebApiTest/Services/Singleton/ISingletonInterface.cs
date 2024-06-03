namespace WebApiTest.Services.Singleton
{
    public interface ISingletonInterface
    {
        public string Value { set;  }
        public string Get();
    }
}
