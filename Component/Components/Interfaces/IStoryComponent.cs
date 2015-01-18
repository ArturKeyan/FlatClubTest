using Component.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Components.Interfaces
{
    public interface IStoryComponent
    {
        bool Add(StoryModel story);
        bool Edit(StoryModel story);
        StoryModel GetDetails(int id);
        IEnumerable<StoryModel> GetByUser(int userId);
        IEnumerable<StoryModel> GetByGroup(int groupId);
    }
}
