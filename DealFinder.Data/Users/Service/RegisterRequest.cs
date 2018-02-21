namespace DealFinder.Data.Users.Service
{
    public class RegisterRequest
    {
        public string UserToken { get; set; }
        public Authenticator Authenticator { get; set; }
    }
}