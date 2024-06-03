using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services.Scoped;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("ScopedTest")]
    public class ScopedTestController : ControllerBase
    {
        private readonly ILogger<ScopedTestController> _logger;
        private readonly ScopedNoInterface ScopedNoInterfaceService;
        private readonly IScopedInterface ScopedInterfaceService;


        public ScopedTestController(ILogger<ScopedTestController> logger, ScopedNoInterface scopedNoInterface, IScopedInterface scopedInterface)
        {
            _logger = logger;
            this.ScopedInterfaceService = scopedInterface;
            this.ScopedNoInterfaceService = scopedNoInterface;
        }

        [Route("NoInterfaceGet")]
        [HttpGet]
        public string NoInterfaceGet()
        {
            return ScopedNoInterfaceService.Get();
        }

        [Route("NoInterfaceSet")]
        [HttpPost]
        public void NoInterfaceSet(string value)
        {
            ScopedNoInterfaceService.Value = value;
        }

        [Route("InterfaceGet")]
        [HttpGet]
        public string InterfaceGet()
        {
            return ScopedInterfaceService.Get();
        }

        [Route("InterfaceSet")]
        [HttpPost]
        public void InterfaceSet(string value)
        {
            ScopedInterfaceService.Value = value;
        }
    }
}
