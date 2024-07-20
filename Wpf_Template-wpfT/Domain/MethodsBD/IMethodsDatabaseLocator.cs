using Domain.UserBd;

namespace Domain.MethodsBD
{
    public interface IMethodsDatabaseLocator
    {
        public MethodsDbContext Context { get; set; }
    }
}