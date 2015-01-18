using DataAccess.Entity;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Get(string name, string password);
        User Get(string name);
    }
}
