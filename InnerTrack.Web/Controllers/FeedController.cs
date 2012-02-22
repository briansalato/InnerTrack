using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Web.Models;
using InnerTrack.Web.Helpers;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.Web.Controllers
{
    public class FeedController : BaseController
    {
        [HttpGet]
        public ActionResult Show(int id)
        {
            var obj = FeedLogic.Get(id);
            if (obj == null)
            {
                this.AddError("FeedNotFound", "That feed could not be found");
                return Redirect(Url.Project_Index());
            }
            var model = ModelConverter.ConvertToShow(obj);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int projectId)
        {
            var projObj = ProjectLogic.Get(projectId);
            if (projObj == null)
            {
                this.AddError("ProjectNotFound", "That project could not be found");
                return Redirect(Url.Project_Index());
            }

            var model = new EditFeedModel
            {
                ProjectId = projectId
            };

            var types = FeedLogic.GetFeedTypes();
            model.AvailableTypes = types.ToList().Select(t => new SelectListItem() { Text = t.ToDescription(), Value = ((int)t).ToString() });
            
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Create(EditFeedModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Feed_Create(model.ProjectId));
            }
            var obj = ModelConverter.Convert(model);
            obj.Id = FeedLogic.Create(obj, CurrentUserName);
            if (!obj.Id.HasValue || obj.Id < 0)
            {
                this.AddError("ErrorCreatingFeed", "There was an error creating your feed.");
                return Redirect(Url.Feed_Create(model.ProjectId));
            }
            return Redirect(Url.Feed_Show(obj.Id.Value));
        }

        #region -Constructors
        [ExcludeFromCodeCoverage]
        public FeedController() : this(new BLL.Logic.FeedLogic(), new BLL.Logic.ProjectLogic()) { }

        [ExcludeFromCodeCoverage]
        public FeedController(IFeedLogic feedLogic, IProjectLogic projectLogic)
        {
            FeedLogic = feedLogic;
            ProjectLogic = projectLogic;
        }
        #endregion

        #region -Properties
        private IFeedLogic FeedLogic { get; set; }
        private IProjectLogic ProjectLogic { get; set; }
        #endregion
    }
}
