using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Controllers
{
    public class PageController: Controller
    {
        private readonly ILogger<PageController> logger;
        public PageController(ILogger<PageController> logger)
        {
            this.logger = logger;
        }
        [Route("")]
        [Route("Pages/{page}")]
        public IActionResult OpenPage(string page)
        {
            logger.LogDebug("OpenPage " + page);
            return View("Views/_Host.cshtml");
        }
        [Authorize]
        [Route("Sample")]
        public string SampleMessage()
        {
            return "Hello";
        }
    }
}