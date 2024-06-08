using FluentDI;

namespace WebApiTest.Services.Transient
{
    [Injector<TimeService>(ServiceLifetime.Transient)]
    public class TimeService
    {
        public string GetTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}
