using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<ITimeOfDayService>]
    public class DawnOfDay : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(0, 0, 0);

        public TimeOnly To => new TimeOnly(8, 0, 0);

        public string GetTimeOfDay()
        {
            return "It's dawn!";
        }
    }
}