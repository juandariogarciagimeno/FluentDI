using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<ITimeOfDayService>]
    public class NightOfDay : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(18, 0, 0);

        public TimeOnly To => new TimeOnly(23, 59, 0);

        public string GetTimeOfDay()
        {
            return "It's nighttime!";
        }
    }
}