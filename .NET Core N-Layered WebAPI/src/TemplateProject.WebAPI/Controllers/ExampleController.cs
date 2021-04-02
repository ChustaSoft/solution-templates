using ChustaSoft.Common.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TemplateProject.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ApiControllerBase<ExampleController>
    {
        public ExampleController(ILogger<ExampleController> logger)
           : base(logger)
        {
            
        }
    }
}
