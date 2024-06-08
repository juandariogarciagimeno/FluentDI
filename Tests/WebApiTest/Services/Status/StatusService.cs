using FluentDI;

namespace WebApiTest.Services.Singleton
{
    [Injector<IStatusService>(ServiceLifetime.Singleton)]
    public class StatusService : IStatusService
    {
        private string Status { get; set; }

        public StatusService()
        {
            Status = "Active";
        }

        public string GetStatus()
        {
            return Status;
        }

        public void SetStatus(string value)
        {
            Status = value;
        }
    }
}