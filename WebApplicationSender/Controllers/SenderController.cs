using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationSender.Services;

namespace WebApplicationSender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SenderController : ControllerBase
    {
        private readonly ILogger<SenderController> _logger;
        private readonly SenderService _senderService;

        public SenderController(ILogger<SenderController> logger, SenderService senderService)
        {
            _logger = logger;
            _senderService = senderService;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            _logger.LogInformation($"{nameof(Send)} invoked");

            await _senderService.SendAsync();

            return NoContent();
        }
    }
}
