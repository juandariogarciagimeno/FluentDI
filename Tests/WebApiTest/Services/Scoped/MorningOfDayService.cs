using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<ITimeOfDayService>]
    public class MorningOfDayService : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(8, 0, 0);

        public TimeOnly To => new TimeOnly(12, 0, 0);

        public string GetTimeOfDay()
        {
            return "It's daytime!";
        }
    }
}