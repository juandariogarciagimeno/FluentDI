namespace WebApiTest.Services.Singleton
{
    public interface IStatusService
    {
        public string GetStatus();

        public void SetStatus(string value);
    }
}
