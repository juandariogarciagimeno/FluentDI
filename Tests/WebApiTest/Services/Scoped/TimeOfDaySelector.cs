using FluentDI.Interfaces;

namespace WebApiTest.Services.Scoped
{
    public class TimeOfDaySelector : IRuntimeDependencySelector<ITimeOfDayService>
    {
        public Func<ITimeOfDayService, object[], bool> CanInject => (s, param) =>
        {
            if (param.Length == 0 || param[0] is not DateTime date)
                throw new ArgumentException("Param must be a date");

            var time = TimeOnly.FromDateTime(date);
            
            return time >= s.From && time <= s.To;
        };
    }
}
