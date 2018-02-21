using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Deals.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Deal
{
    [Route("api/[controller]")]
    public class DealController : Controller
    {
        private readonly IDealsService _dealsService;

        public DealController()
        {
            _dealsService = new DealsService(new DealsRepository());
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