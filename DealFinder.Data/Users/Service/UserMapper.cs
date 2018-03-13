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
            double.TryParse(_encryptor.Decrypt(user.Latitude), out var latitude);
            double.TryParse(_encryptor.Decrypt(user.Longitude), out var longitude);

            return new UserModel
            {
                Identifier = user.Identifier, 
                Username = _encryptor.Decrypt(user.Username),
                Picture = _encryptor.Decrypt(user.Picture),
                Latitude = latitude,
                Longitude = longitude
            };
        }
    }
}