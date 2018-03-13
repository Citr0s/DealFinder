using System;
using System.Linq;
using DealFinder.Core.Communication;
using DealFinder.Core.Security;

namespace DealFinder.Data.Users.Repository
{
    public interface IUserRepository
    {
        CreateUserResponse CreateUser(CreateUserRequest request);
        GetUserResponse GetUser(string userToken);
        UpdateUserResponse UpdateUser(UpdateUserRequest updateUserRequest);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IAesEncryptor _encryptor;
        private readonly IKeyReader _keyReader;

        public UserRepository(IAesEncryptor encryptor, IKeyReader keyReader)
        {
            _encryptor = encryptor;
            _keyReader = keyReader;
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(x => _encryptor.Decrypt(x.UserToken, _keyReader.GetKey()) == request.User.UserToken);

                    if (user != null)
                        throw new UserAlreadyExistsException("User has already been registered");

                    context.Add(new UserRecord
                    {
                        UserToken = _encryptor.Encrypt(request.User.UserToken, _keyReader.GetKey()),
                        Username = _encryptor.Encrypt(request.User.Username, _keyReader.GetKey()),
                        Picture = _encryptor.Encrypt(request.User.Picture, _keyReader.GetKey())
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (UserAlreadyExistsException exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.UserAlreadyExists,
                        UserMessage = "User with that UserToken already exists.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong when creating your account. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }

        public GetUserResponse GetUser(string userToken)
        {
            var response = new GetUserResponse();

            using (var context = new DatabaseContext())
            {
                try
                {
                    response.User = context.Users.First(x => x.UserToken == userToken);
                }
                catch (Exception exception)
                {
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong when retrieving your account. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.First(x => x.Identifier == request.User.Identifier);
                    user.Username = _encryptor.Encrypt(request.User.Username, _keyReader.GetKey());
                    response.User = user;

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong when updating your account. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }
    }
}