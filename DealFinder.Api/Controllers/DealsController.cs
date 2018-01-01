using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers
{
    [Route("api/[controller]")]
    public class DealsController : Controller
    {
        [HttpGet("{latitude}/{longitude}")]
        public ActionResult Get(double latitude, double longitude)
        {
            return Ok(new
            {
                Latitude = latitude,
                Longitude = longitude
            });
        }
    }
}