using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        void Edit(Group group);
        bool AddMember(int groupId, User user);
        bool RemoveMember(int groupId, User user);
        Group GetDetails(int id);
        IEnumerable<Group> GetDetails();
        IEnumerable<Group> GetDetailsByUserId(int userId);
    }
}
