using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiTest.Services.Scoped;
using WebApiTest.Services.Transient;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Time")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        private TimeService TimeService;


        public TimeController(ILogger<TimeController> logger, TimeService transientNoInterface)
        {
            _logger = logger;
            this.TimeService = transientNoInterface;
        }

        [Route("Get")]
        [HttpGet]
        public string Get()
        {
            return TimeService.GetTime();
        }
    }
}
