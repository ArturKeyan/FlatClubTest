using DataAccess.Entity;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        void Edit(Story story);
        Story GetDetails(int id);
        IEnumerable<Story> GetByUserId(int userId);
        IEnumerable<Story> GetByGroupId(int groupId);
    }
}
