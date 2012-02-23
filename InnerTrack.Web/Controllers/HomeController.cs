using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.Web.Models;

namespace InnerTrack.Web.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.ProjectStatuses = new List<ListItemProjectStatusModel>();
            model.FeedEvents = new List<ListItemEventModel>();

            return View(model);
        }
    }
}
