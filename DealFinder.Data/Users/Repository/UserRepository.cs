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
    }

    public class UserRepository : IUserRepository
    {
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(x => AesEncryptor.Decrypt(x.UserToken, KeyReader.Instance().GetKey()) == request.User.UserToken);

                    if(user != null)
                        throw new Exception("User has already been registered");

                    context.Add(new UserRecord
                    {
                        UserToken = AesEncryptor.Encrypt(request.User.UserToken, KeyReader.Instance().GetKey()),
                        Username = AesEncryptor.Encrypt(request.User.Username, KeyReader.Instance().GetKey()),
                        Picture = AesEncryptor.Encrypt(request.User.Picture, KeyReader.Instance().GetKey())
                    });
                    context.SaveChanges();
                    transaction.Commit();
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
                    response.User = context.Users.First(x => AesEncryptor.Decrypt(x.UserToken, KeyReader.Instance().GetKey()) == userToken);
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
    }
}