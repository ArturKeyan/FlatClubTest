using Component.Components.Interfaces;
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
    public class UserController : BaseController
    {
        private IUserComponent userComponent;

        public UserController(IUserComponent userComponent)
        {
            this.userComponent = userComponent;
        }
        
        //
        // GET: /User/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /User/Login
        [HttpPost]
        public ActionResult Login(LoginVModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        //
        // GET: /User/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /User/Register
        [HttpPost]
        public ActionResult Register(RegisterVModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status;
                var membershipUser = Membership.CreateUser(model.UserName, model.Password, null, null, null, true, out status);

                if (status == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", status.ToString());
                }
            }

            return View(model);
        }

        //
        // POST: /User/Logout
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {                 
                return Redirect("/");
            }
        }
    }
}
