using Component.Components.Interfaces;
using Component.Models;
using FlatClub.MemberhipProvider;
using FlatClub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FlatClub.Controllers
{
    [Authorize]
    public class StoryController : BaseController
    {        
        private IStoryComponent storyComponent;
        private IGroupComponent groupComponent;

        public StoryController(IStoryComponent storyComponent, IGroupComponent groupComponent)
        {
            this.storyComponent = storyComponent;
            this.groupComponent = groupComponent;
        }

        //
        // GET: /Story
        public ActionResult Index()
        {
            var model = storyComponent.GetByUser(CurrentUserId).Select(m => new StoryVModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Content = m.Content,
                PostedOn = m.PostedOn
            });

            return View(model);
        }
           
        //
        // GET: /Story/Create        
        [HttpGet]
        public ActionResult Create()
        {
            var model = new StoryCreateVModel
            {
                GroupListItems = GetGroupsListItems(),
            };

            return View(model);
        }

        //
        // POST: /Story/Create
        [HttpPost]
        public ActionResult Create(StoryCreateVModel model)
        {
            if (ModelState.IsValid)
            {
                var result = storyComponent.Add(new StoryModel
                {
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    CreatorId = CurrentUserId,
                    GroupIds = model.GroupIds,                    
                });

                if (result)
                {
                    return RedirectToAction("Index", "Story");
                }
                else
                {
                    ModelState.AddModelError("", "Story creation failed");
                }
            }

            model.GroupListItems = GetGroupsListItems();
            return View(model);
        }
        
        //
        // GET: /Story/Edit    
        public ActionResult Edit(int id)
        {
            var story = storyComponent.GetDetails(id);

            if (story != null)
            {
                var model = new StoryEditVModel
                {
                    Id = story.Id,
                    Title = story.Title,
                    Description = story.Description,
                    Content = story.Content,
                    GroupIds = story.Groups.Select(m => m.Id).ToArray(),
                    GroupListItems = GetGroupsListItems()
                };

                return View(model);                
            }

            else return RedirectToAction("Index", "Story"); 
        }


        //
        // POST: /Story/Edit
        [HttpPost]
        public ActionResult Edit(StoryEditVModel model)
        {
            if (ModelState.IsValid)
            {
                var result = storyComponent.Edit(new StoryModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    GroupIds = model.GroupIds,
                });

                if (result)
                {
                    return RedirectToAction("Index", "Story");
                }
                else
                {
                    ModelState.AddModelError("", "Story creation failed");
                }
            }

            model.GroupListItems = GetGroupsListItems();
            return View(model);
        }


        //
        // GET: /Story/Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var story = storyComponent.GetDetails(id);

            if (story != null)
            {
                var model = new StoryDetailsVModel
                {
                    Id = story.Id,
                    Title = story.Title,
                    Description = story.Description,
                    Content = story.Content,
                    PostedOn = story.PostedOn,
                    IsEditable = story.CreatorId == CurrentUserId,
                    Creator = new UserVModel
                    {
                        Id = story.Creator.Id,
                        Name = story.Creator.Name
                    },
                    Groups = story.Groups.Select(m => new GroupVModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description = m.Description
                    }).ToArray()
                };

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Story was not found");
            }

            return RedirectToAction("Index", "Story");
        }
        public IEnumerable<SelectListItem> GetGroupsListItems()
        {
            return groupComponent.Get().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });
        }
    }
}
