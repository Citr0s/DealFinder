using DealFinder.Data.ThirdPartyAuthenticator;
using DealFinder.Data.Users.Repository;
using DealFinder.Data.Users.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Users
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService(new UserRepository(), new AuthenticatorFactory());
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]RegisterRequest request)
        {
            return Ok(_userService.Register(request));
        }
    }
}
