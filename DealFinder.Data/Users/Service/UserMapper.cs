using DealFinder.Core.Security;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Users.Service
{
    public interface IUserMapper
    {
        UserModel Map(UserRecord user);
    }

    public class UserMapper: IUserMapper
    {
        private readonly IAesEncryptor _encryptor;

        public UserMapper(IAesEncryptor encryptor)
        {
            _encryptor = encryptor;
        }

        public UserModel Map(UserRecord user)
        {
            return new UserModel
            {
                Identifier = user.Identifier, 
                Username = _encryptor.Decrypt(user.Username),
                Picture = _encryptor.Decrypt(user.Picture)
            };
        }
    }
}