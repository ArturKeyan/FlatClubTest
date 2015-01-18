using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Models
{
    public class StoryModel : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public int CreatorId { get; set; }
        public int[] GroupIds { get; set; }

        public UserModel Creator { get; set; }
        public IEnumerable<GroupModel> Groups { get; set; }
        
        public override bool Equals(object obj)
        {
            var other = obj as StoryModel;


            var aaa = Equals(this.Creator, other.Creator);

            if (other != null)
            {
                return
                    this.Id == other.Id &&
                    this.Title == other.Title &&
                    this.Description == other.Description &&
                    this.Content == other.Content &&
                    this.PostedOn == other.PostedOn &&
                    this.CreatorId == other.CreatorId &&
                    this.GroupIds == other.GroupIds &&
                    Equals(this.Creator, other.Creator) && 
                    Equals(this.Groups, other.Groups);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public static explicit operator StoryModel(Story entry)
        {
            var story = new StoryModel
            {
                Id = entry.Id,
                Title = entry.Title,
                Description = entry.Description,
                Content = entry.Content,
                CreatorId = entry.CreatorId,
                PostedOn = entry.PostedOn
            };

            if (entry.Creator != null)
            {
                story.Creator = new UserModel
                {
                    Id = entry.Creator.Id,
                    Name = entry.Creator.Name
                };            
            }

            if (entry.Groups != null)
            {
                story.Groups = entry.Groups.Select(m => new GroupModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description
                });
            }

            return story;
        }
    }
}
