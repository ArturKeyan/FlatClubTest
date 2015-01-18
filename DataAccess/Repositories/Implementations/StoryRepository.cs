using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace DataAccess.Repositories.Implementations
{
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        public StoryRepository(FlatClubContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }
        public void Edit(Story story)
        {
            var updatedStory = GetByID(story.Id);

            // clearing old groups
            updatedStory.Groups.Clear();
            ctx.SaveChanges();

            updatedStory.Title = story.Title;
            updatedStory.Description = story.Description;
            updatedStory.Content = story.Content;
            updatedStory.Groups = story.Groups;

            ctx.Entry(updatedStory).State = EntityState.Modified;
        }

        public Story GetDetails(int id)
        {
            return ctx.Stories.Include(m => m.Creator).Include(m => m.Groups).FirstOrDefault(m => m.Id == id);
        }
        
        public IEnumerable<Story> GetByUserId(int userId)
        {
            return ctx.Stories.Include(m => m.Creator).Where(m => m.CreatorId == userId);
        }

        public IEnumerable<Story> GetByGroupId(int groupId)
        {
            var group = ctx.Groups.Include(m => m.Stories.Select(l => l.Creator)).SingleOrDefault(m => m.Id == groupId);
            return group != null ? group.Stories : null;
        }
    }
}
