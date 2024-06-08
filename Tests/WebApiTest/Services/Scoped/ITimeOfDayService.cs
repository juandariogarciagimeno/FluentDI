namespace WebApiTest.Services.Scoped
{
    public interface ITimeOfDayService
    {
        public TimeOnly From { get; }
        public TimeOnly To { get; }
        public string GetTimeOfDay();
    }
}
