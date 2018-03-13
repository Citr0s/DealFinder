using System;
using DealFinder.Core.Communication;
using DealFinder.Data.ThirdPartyAuthenticator;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Users.Service
{
    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest request);
        UpdateResponse Update(UpdateRequest request);
        SaveLocationResponse SaveLastKnownLocation(double latitude, double longitude, string userIdentifier);
        DeleteResponse Delete(string userIdentifier);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticatorFactory _authenticator;
        private readonly IUserMapper _userMapper;

        public UserService(IUserRepository userRepository, IAuthenticatorFactory authenticator, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
            _userMapper = userMapper;
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

            response.User = _userMapper.Map(getUserResponse.User);
            return response;
        }

        public UpdateResponse Update(UpdateRequest request)
        {
            var response = new UpdateResponse();

            var createUserResponse = _userRepository.UpdateUser(new UpdateUserRequest
            {
                User = request.User
            });

            if (createUserResponse.HasError)
            {
                response.AddError(createUserResponse.Error);
                return response;
            }

            var getUserResponse = _userRepository.GetUser(createUserResponse.User.UserToken);

            if (getUserResponse.HasError)
            {
                response.AddError(getUserResponse.Error);
                return response;
            }

            response.User = _userMapper.Map(getUserResponse.User);

            return response;
        }

        public SaveLocationResponse SaveLastKnownLocation(double latitude, double longitude, string userIdentifier)
        {
            var response = new SaveLocationResponse();

            if (!Guid.TryParse(userIdentifier, out var userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.InvalidGuidProvided,
                    UserMessage = "Something went wrong. Please try again later",
                    TechnicalMessage = $"Invalid User Identifier provided {userIdentifier}"
                });
                return response;
            }

            var updateUserResponse = _userRepository.UpdateUser(new UpdateUserRequest
            {
                User = new UserModel
                {
                    Identifier = userId,
                    Latitude = latitude,
                    Longitude = longitude
                }
            });

            if (updateUserResponse.HasError)
            {
                response.AddError(updateUserResponse.Error);
                return response;
            }

            var getUserResponse = _userRepository.GetUser(updateUserResponse.User.UserToken);

            if (getUserResponse.HasError)
            {
                response.AddError(getUserResponse.Error);
                return response;
            }

            response.User = _userMapper.Map(getUserResponse.User);

            return response;
        }

        public DeleteResponse Delete(string userIdentifier)
        {
            var response = new DeleteResponse();

            if (!Guid.TryParse(userIdentifier, out var userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.InvalidGuidProvided,
                    UserMessage = "Something went wrong. Please try again later",
                    TechnicalMessage = $"Invalid User Identifier provided {userIdentifier}"
                });
                return response;
            }

            var deleteUserResponse = _userRepository.DeleteUser(userId);

            if(deleteUserResponse.HasError)
                response.AddError(deleteUserResponse.Error);

            return response;
        }
    }
}