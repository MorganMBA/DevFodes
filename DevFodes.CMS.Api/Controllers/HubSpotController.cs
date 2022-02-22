using DevFodes.CMS.Business.Client;
using DevFodes.CMS.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DevFodes.CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubSpotController : ControllerBase
    {
        private IHubSpotClient _hubSpotClient;
        private ILogger _logger;

        public HubSpotController(ILogger<HubSpotController> logger,
            IHubSpotClient hubSpotClient)
        {
            _logger = logger;
            _hubSpotClient = hubSpotClient;
        }

        [HttpPost("PostContact")]
        public async Task<string> PostContactHubSpot(Contact contactRequest)
        {
            using var scope = _logger.BeginScope("{request}", contactRequest);

            return await _hubSpotClient.PostContactAsync(contactRequest);
        }
    }
}
