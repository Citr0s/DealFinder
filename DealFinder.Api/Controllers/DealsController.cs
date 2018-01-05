using DealFinder.Data.Deals;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers
{
    [Route("api/[controller]")]
    public class DealsController : Controller
    {
        private readonly IDealsService _dealsService;
        
        public DealsController()
        {
            _dealsService = new DealsService(new DealsRepository());
        }
        
        [HttpGet("{latitude}/{longitude}")]
        public ActionResult Get(double latitude, double longitude)
        {
            return Ok(_dealsService.GetByLocation(latitude, longitude));
        }
    }
}