using DealFinder.Core.Communication;
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

            var createUserResponse =_userRepository.CreateUser(new CreateUserRequest
            {
                User = new UserModel
                {
                    UserToken = authenticationResponse.UserId,
                    Username = authenticationResponse.Username,
                    Picture = authenticationResponse.Picture
                }
            });

            if (createUserResponse.HasError && createUserResponse.Error.Code != ErrorCodes.UserAlreadyExists)
            {
                response.AddError(createUserResponse.Error);
                return response;
            }

            var getUserResponse = _userRepository.GetUser(authenticationResponse.UserId);

            if (getUserResponse.HasError)
            {
                response.AddError(getUserResponse.Error);
                return response;
            }

            response.User = UserMapper.Map(getUserResponse.User);
            return response;
        }
    }
}