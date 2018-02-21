using DealFinder.Data.Users.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Users
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]RegisterRequest request)
        {
            return Ok(_userService.Register(request));
        }
    }
}
