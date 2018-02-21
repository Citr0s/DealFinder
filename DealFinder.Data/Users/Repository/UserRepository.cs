namespace DealFinder.Data.Users.Repository
{
    public interface IUserRepository
    {
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
    }
}