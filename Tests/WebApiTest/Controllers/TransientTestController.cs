using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiTest.Services.Scoped;
using WebApiTest.Services.Transient;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("TransientTest")]
    public class TransientTestController : ControllerBase
    {
        private readonly ILogger<TransientTestController> _logger;
        private TransientNoInterface TransientNoInterfaceService;
        private ITransientInterface TransientInterfaceService;
        private readonly IHttpContextAccessor httpContextAccessor;


        public TransientTestController(ILogger<TransientTestController> logger, IHttpContextAccessor accessor, TransientNoInterface transientNoInterface, ITransientInterface transietInterface)
        {
            _logger = logger;
            this.TransientInterfaceService = transietInterface;
            this.TransientNoInterfaceService = transientNoInterface;
            this.httpContextAccessor = accessor;
        }

        [Route("NoInterfaceGet")]
        [HttpGet]
        public string NoInterfaceGet()
        {
            TransientNoInterfaceService.Value = "aaaaa";

            TransientNoInterfaceService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<TransientNoInterface>();

            return TransientNoInterfaceService.Get();
        }

        [Route("NoInterfaceSet")]
        [HttpPost]
        public void NoInterfaceSet(string value)
        {
            TransientNoInterfaceService.Value = value;
        }

        [Route("InterfaceGet")]
        [HttpGet]
        public string InterfaceGet()
        {
            TransientInterfaceService.Value = "aaaaa";

            TransientInterfaceService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITransientInterface>();

            return TransientInterfaceService.Get();
        }

        [Route("InterfaceSet")]
        [HttpPost]
        public void InterfaceSet(string value)
        {
            TransientInterfaceService.Value = value;
        }
    }
}
