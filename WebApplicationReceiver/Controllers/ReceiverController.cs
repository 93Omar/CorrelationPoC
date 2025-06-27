using Microsoft.AspNetCore.Mvc;

namespace WebApplicationReceiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiverController : ControllerBase
    {
        private readonly ILogger<ReceiverController> _logger;

        public ReceiverController(ILogger<ReceiverController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Receive()
        {
            _logger.LogInformation($"{nameof(Receive)} invoked");

            return NoContent();
        }
    }
}
