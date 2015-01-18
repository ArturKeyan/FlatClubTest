using Component.Components.Interfaces;
using Component.Models;
using DataAccess.Entity;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Component.Components.Implementations
{
    public class StoryComponent : ComponentBase, IStoryComponent
    {
        public StoryComponent(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool Add(StoryModel story)
        {
            try
            {
                if (story != null && story.GroupIds.Count() > 0)
                {
                    unit.StoryRepository.Insert(new Story
                    {
                        Title = story.Title,
                        Description = story.Description,
                        Content = story.Content,
                        CreatorId = story.CreatorId,
                        PostedOn = DateTime.UtcNow,
                        Groups = story.GroupIds.Select(m => unit.GroupRepository.GetByID(m)).ToArray()
                    });

                    unit.Save();

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool Edit(StoryModel story)
        {
            try
            {
                if (story != null && story.GroupIds.Count() > 0)
                {
                    unit.StoryRepository.Edit(new Story
                    {
                        Id = story.Id,
                        Title = story.Title,
                        Description = story.Description,
                        Content = story.Content,
                        Groups = story.GroupIds.Select(m => unit.GroupRepository.GetByID(m)).ToArray()
                    });

                    unit.Save();

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IEnumerable<StoryModel> GetByUser(int userId)
        {
            try
            {
                var stories = unit.StoryRepository.GetByUserId(userId);

                if (stories != null)
                {
                    return stories.Select(m => (StoryModel)m);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public IEnumerable<StoryModel> GetByGroup(int groupId)
        {
            try
            {
                var stories = unit.StoryRepository.GetByGroupId(groupId);

                if (stories != null)
                {
                    return stories.Select(m => (StoryModel)m);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }


        public StoryModel GetDetails(int id)
        {
            try
            {
                var story = unit.StoryRepository.GetDetails(id);

                if (story != null)
                {
                    return (StoryModel)story;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
