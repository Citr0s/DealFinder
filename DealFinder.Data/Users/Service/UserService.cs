using DealFinder.Data.ThirdPartyAuthenticator;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Users.Service
{
    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticatorFactory _authenticator;

        public UserService(UserContext context) : this(new UserRepository(context), new AuthenticatorFactory()) { }

        public UserService(IUserRepository userRepository, IAuthenticatorFactory authenticator)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var response = new RegisterResponse();

            var authenticationResponse = _authenticator.For(request.Authenticator).Authenticate(new AuthenticationRequest
            {
                Token = request.UserToken
            });

            if (authenticationResponse.HasError)
            {
                response.AddError(authenticationResponse.Error);
                return response;
            }
            
            // add user to db

            return response;
        }
    }
}