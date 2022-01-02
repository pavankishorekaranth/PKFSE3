using Buyer.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Buyer.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly IBidService bidService;
        private readonly ILogger<BuyerController> _logger;

        public BuyerController(IBidService bidService, ILogger<BuyerController> logger)
        {
            this.bidService = bidService;
            _logger = logger;
        }
    }
}
