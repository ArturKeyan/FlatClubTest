using Component.Components.Interfaces;
using Component.Models;
using DataAccess.UnitOfWork;
using FlatClub.MemberhipProvider;
using FlatClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FlatClub.Controllers
{
    //[Authorize]
    public class GroupController : BaseController
    {   
        private IGroupComponent groupComponent;

        public GroupController(IGroupComponent groupComponent)
        {
            this.groupComponent = groupComponent;
        }

        //
        // GET: /Group/
        public ActionResult Index()
        {
            var model = groupComponent.Get().Select(m => new GroupVModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                MembersCount = m.Members.Count(),
                StoryesCount = m.Stories.Count(),
                IsJoined = m.Members.Any(l => l.Id == CurrentUserId),
            });

            return View(model.OrderByDescending(m => m.IsJoined));
        }

        //
        // GET: /Group/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create
        [HttpPost]
        public ActionResult Create(GroupCreateVModel model)
        {
            if (ModelState.IsValid)
            {
                var result = groupComponent.Add(new GroupModel
                {
                    Name = model.Name,
                    Description = model.Description
                }, CurrentUserId);

                if (result)
                {
                    return RedirectToAction("Index", "Group");
                }
                else
                {
                    ModelState.AddModelError("", "Group creation failed");
                }
            }

            return View(model);
        }
        
        //
        // GET: /Group/Edit    
        public ActionResult Edit(int id)
        {
            var group = groupComponent.GetDetails(id);

            if (group != null)
            {
                var model = new GroupEditVModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description
                };

                return View(model);
            }

            else return RedirectToAction("Index", "Group");
        }
        
        //
        // POST: /Group/Edit
        [HttpPost]
        public ActionResult Edit(GroupEditVModel model)
        {
            if (ModelState.IsValid)
            {
                var result = groupComponent.Edit(new GroupModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                });

                if (result)
                {
                    return RedirectToAction("Index", "Group");
                }
                else
                {
                    ModelState.AddModelError("", "Group creation failed");
                }
            }

            return View(model);
        }


        //
        // GET: /Group/Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var group = groupComponent.GetDetails(id);

            if (group != null)
            {
                var model = new GroupDetailsVModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    MembersCount = group.Members.Count(),
                    StoryesCount = group.Stories.Count(),
                    IsJoined = group.Members.Any(l => l.Id == CurrentUserId),
                    Members = group.Members.Select(m => new UserVModel
                    {
                        Id = m.Id,
                        Name = m.Name
                    }),
                    Stories = group.Stories.Select(m => new StoryVModel { 
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        Description = m.Description,
                        PostedOn = m.PostedOn,
                    })
                };

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Group was not found");
            }

            return RedirectToAction("Index", "Group");
        }

        [HttpGet]
        public ActionResult Join(int id)
        {
            var result = groupComponent.AddMember(id, CurrentUserId);

            if (result)
            {
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Details", "Group", new { id = id });
        }



        [HttpGet]
        public ActionResult Leave(int id)
        {
            var result = groupComponent.RemoveMember(id, CurrentUserId);

            if (result)
            {
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Details", "Group", new { id = id });
        }

    }
}
