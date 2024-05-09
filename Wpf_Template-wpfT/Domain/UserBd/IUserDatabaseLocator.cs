using Domain.UserBd;

namespace Bootstrapper.UserBd
{
    public interface IUserDatabaseLocator
    {
        public UserDbContext Context { get; set; }
    }
}