using DataAccess.UnitOfWork;
using FlatClub.MemberhipProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FlatClub.Controllers
{
    public abstract class BaseController : Controller
    {
        public int CurrentUserId
        {
            get
            {
                var user = Membership.GetUser();
                return user != null ? (int)user.ProviderUserKey : -1;
            }
        }

    }
}
