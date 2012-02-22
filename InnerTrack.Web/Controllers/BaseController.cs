using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnerTrack.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string CurrentUserName
        {
            get
            {
                return User.Identity.Name;
            }
        }
    }
}
