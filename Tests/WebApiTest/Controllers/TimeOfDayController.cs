using FluentDI.Factory;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services.Scoped;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("TimeOfDay")]
    public class TimeOfDayController : ControllerBase
    {
        private readonly ILogger<TimeOfDayController> _logger;
        private readonly ITimeOfDayService TimeOfDayService;


        public TimeOfDayController(ILogger<TimeOfDayController> logger, DependencyFactory<ITimeOfDayService> dependencyFactory)
        {
            _logger = logger;
            this.TimeOfDayService = dependencyFactory.Get(DateTime.Now);
        }

        [Route("Get")]
        [HttpGet]
        public string Get()
        {
            return TimeOfDayService.GetTimeOfDay();
        }
    }
}
