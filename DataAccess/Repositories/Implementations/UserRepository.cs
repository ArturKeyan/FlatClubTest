using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using System.Linq;

namespace DataAccess.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FlatClubContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public User Get(string name, string password)
        {
            return ctx.Users.FirstOrDefault(m => m.Name == name && m.Password == password);
        }
        
        public User Get(string name)
        {
            return ctx.Users.FirstOrDefault(m => m.Name == name);
        }
    }
}