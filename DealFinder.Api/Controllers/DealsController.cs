using DealFinder.Data.Deals.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers
{
    [Route("api/[controller]")]
    public class DealsController : Controller
    {
        private readonly IDealsService _dealsService;

        public DealsController(IDealsService dealsService)
        {
            _dealsService = dealsService;
        }

        [HttpGet("{latitude}/{longitude}")]
        public ActionResult Get(double latitude, double longitude)
        {
            return Ok(_dealsService.GetByLocation(latitude, longitude));
        }

        [HttpPost("")]
        public ActionResult Post([FromBody]DealModel deal)
        {
            return Ok(_dealsService.SaveDealDetails(deal));
        }
    }
}