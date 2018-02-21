using DealFinder.Core.Security;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Users.Service
{
    public class UserMapper
    {
        public static UserModel Map(UserRecord user)
        {
            return new UserModel
            {
                Identifier = user.Identifier, 
                Username = AesEncryptor.Decrypt(user.Username, KeyReader.Instance().GetKey()),
                Picture = AesEncryptor.Decrypt(user.Picture, KeyReader.Instance().GetKey())
            };
        }
    }
}