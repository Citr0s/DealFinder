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
        private readonly IKeyReader _keyReader;

        public UserMapper(IAesEncryptor encryptor, IKeyReader keyReader)
        {
            _encryptor = encryptor;
            _keyReader = keyReader;
        }

        public UserModel Map(UserRecord user)
        {
            return new UserModel
            {
                Identifier = user.Identifier, 
                Username = _encryptor.Decrypt(user.Username, _keyReader.GetKey()),
                Picture = _encryptor.Decrypt(user.Picture, _keyReader.GetKey())
            };
        }
    }
}