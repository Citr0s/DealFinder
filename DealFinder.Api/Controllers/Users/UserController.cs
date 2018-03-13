using DealFinder.Core.Security;
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
            _userService = new UserService(new UserRepository(new AesEncryptor(KeyReader.Instance().GetKey())), new AuthenticatorFactory(), new UserMapper(new AesEncryptor(KeyReader.Instance().GetKey())));
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]RegisterRequest request)
        {
            return Ok(_userService.Register(request));
        }

        [HttpPut("update")]
        public ActionResult Put([FromBody]UpdateRequest request)
        {
            return Ok(_userService.Update(request));
        }
    }
}
