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
        DeleteUserResponse DeleteUser(Guid userIdentifier);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IAesEncryptor _encryptor;

        public UserRepository(IAesEncryptor encryptor)
        {
            _encryptor = encryptor;
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(x => _encryptor.Decrypt(x.UserToken) == request.User.UserToken);

                    if (user == null)
                    {
                        context.Add(new UserRecord
                        {
                            UserToken = _encryptor.Encrypt(request.User.UserToken),
                            Username = _encryptor.Encrypt(request.User.Username),
                            Picture = _encryptor.Encrypt(request.User.Picture),
                            Active = true
                        });

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        if (user.Active)
                            throw new UserAlreadyExistsException("User has already been registered");

                        user.UserToken = _encryptor.Encrypt(request.User.UserToken);
                        user.Username = _encryptor.Encrypt(request.User.Username);
                        user.Picture = _encryptor.Encrypt(request.User.Picture);
                        user.Active = true;

                        context.SaveChanges();
                        transaction.Commit();
                    }
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
                    response.User = context.Users.FirstOrDefault(x => _encryptor.Decrypt(x.UserToken) == userToken && x.Active);
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
                    var user = context.Users.First(x => x.Identifier == request.User.Identifier && x.Active);

                    if (request.User.Username != null)
                        user.Username = _encryptor.Encrypt(request.User.Username);

                    if (request.User.Latitude != null && request.User.Longitude != null)
                    {
                        user.Latitude = _encryptor.Encrypt(request.User.Latitude.ToString());
                        user.Longitude = _encryptor.Encrypt(request.User.Longitude.ToString());
                    }

                    response.User = new UserRecord
                    {
                        UserToken = _encryptor.Decrypt(user.UserToken)
                    };

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong while updating your account. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }

        public DeleteUserResponse DeleteUser(Guid userIdentifier)
        {
            var response = new DeleteUserResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.First(x => x.Identifier == userIdentifier);
                    user.Active = false;

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong while deleting your account. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }
    }
}