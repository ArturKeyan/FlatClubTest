using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories.Implementations
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(FlatClubContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public void Edit(Group group)
        {
            var updatedGroup = GetByID(group.Id);

            updatedGroup.Name = group.Name;
            updatedGroup.Description = group.Description;

            ctx.Entry(updatedGroup).State = EntityState.Modified;
        }

        public bool AddMember(int groupId, User user)
        {
            var group = GetByID(groupId);

            if (group != null && user != null && !group.Members.Any(m => m.Id == user.Id))
            {
                group.Members.Add(user);
                ctx.Entry(group).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool RemoveMember(int groupId, User user)
        {
            var group = GetByID(groupId);

            if (group != null && user != null && group.Members.Any(m => m.Id == user.Id))
            {
                var result = group.Members.Remove(user);
                ctx.Entry(group).State = EntityState.Modified;
                return result;
            }
            return false;
        }

        public IEnumerable<Group> GetDetails()
        {
            return ctx.Groups.Include(m => m.Members).Include(m => m.Stories);
        }

        public Group GetDetails(int id)
        {
            return ctx.Groups.Include(m => m.Members).Include(m => m.Stories).FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Group> GetDetailsByUserId(int userId)
        {
            var user = ctx.Users.Include(m => m.Groups.Select(l => l.Stories)).SingleOrDefault(m => m.Id == userId);
            return user != null ? user.Groups : null;
        }
    }
}
