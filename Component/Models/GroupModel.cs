using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Models
{
    public class GroupModel : BaseModel
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<StoryModel> Stories { get; set; }

        public IEnumerable<UserModel> Members { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as GroupModel;
            
            if (other != null)
            {
                return
                    this.Id == other.Id &&
                    this.Name == other.Name &&
                    this.Description == other.Description &&
                    Equals(this.Stories, other.Stories) &&
                    Equals(this.Members, other.Members);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static explicit operator GroupModel(Group entry)
        {
            var group = new GroupModel
            {
                Id = entry.Id,
                Name = entry.Name,
                Description = entry.Description
            };

            if (entry.Members != null)
            {
                group.Members = entry.Members.Select(l => new UserModel
                {
                    Id = l.Id,
                    Name = l.Name
                });
            }

            if (entry.Stories != null)
            {
                group.Stories = entry.Stories.Select(l => new StoryModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Content = l.Content,
                    CreatorId = l.CreatorId,
                    PostedOn = l.PostedOn
                });
            }

            return group;
        }
    }
}
