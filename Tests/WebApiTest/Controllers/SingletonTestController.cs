using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services.Scoped;
using WebApiTest.Services.Singleton;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("SingletonTest")]
    public class SingletonTestController : ControllerBase
    {
        private readonly ILogger<SingletonTestController> _logger;
        private readonly SingletonNoInterface SingletonNoInterfaceService;
        private readonly ISingletonInterface SingletonInterfaceService;


        public SingletonTestController(ILogger<SingletonTestController> logger, SingletonNoInterface singletonNoInterface, ISingletonInterface singletonInterface)
        {
            _logger = logger;
            this.SingletonInterfaceService = singletonInterface;
            this.SingletonNoInterfaceService = singletonNoInterface;
        }

        [Route("NoInterfaceGet")]
        [HttpGet]
        public string NoInterfaceGet()
        {
            return SingletonNoInterfaceService.Get();
        }

        [Route("NoInterfaceSet")]
        [HttpPost]
        public void NoInterfaceSet(string value)
        {
            SingletonNoInterfaceService.Value = value;
        }

        [Route("InterfaceGet")]
        [HttpGet]
        public string InterfaceGet()
        {
            return SingletonInterfaceService.Get();
        }

        [Route("InterfaceSet")]
        [HttpPost]
        public void InterfaceSet(string value)
        {
            SingletonInterfaceService.Value = value;
        }
    }
}
