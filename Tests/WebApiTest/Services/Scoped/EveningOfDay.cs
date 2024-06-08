using FluentDI;

namespace WebApiTest.Services.Scoped
{
    [Injector<ITimeOfDayService>]
    public class EveningOfDay : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(12, 0, 0);

        public TimeOnly To => new TimeOnly(18, 0, 0);

        public string GetTimeOfDay()
        {
            return "It's evening!";
        }
    }
}
