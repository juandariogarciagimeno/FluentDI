using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services.Scoped;
using WebApiTest.Services.Singleton;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Status")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService StatusService;

        public StatusController(ILogger<StatusController> logger, IStatusService statusService)
        {
            _logger = logger;
            this.StatusService = statusService;
        }

        [Route("Get")]
        [HttpGet]
        public string Get()
        {
            return this.StatusService.GetStatus();
        }

        [Route("Set")]
        [HttpPost]
        public void Set(string value)
        {
            this.StatusService.SetStatus(value);
        }
    }
}
