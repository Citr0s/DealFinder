using DealFinder.Core.Security;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Deals.Service;
using DealFinder.Data.Users.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Deal
{
    [Route("api/[controller]")]
    public class DealController : Controller
    {
        private readonly IDealsService _dealsService;
        
        public DealController()
        {
            _dealsService = new DealsService(new DealsRepository(), new DealsMapper(new UserMapper(new AesEncryptor(), KeyReader.Instance())));
        }

        [HttpGet("{latitude}/{longitude}/{userIdentifier?}")]
        public ActionResult Get(double latitude, double longitude, string userIdentifier)
        {
            return Ok(_dealsService.GetByLocation(latitude, longitude, userIdentifier));
        }

        [HttpPost("")]
        public ActionResult Post([FromBody]DealModel deal)
        {
            return Ok(_dealsService.SaveDealDetails(deal));
        }
    }
}