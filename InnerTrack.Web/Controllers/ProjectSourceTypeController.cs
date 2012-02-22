using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using InnerTrack.Web.Helpers;
using InnerTrack.Web.Models;

namespace InnerTrack.Web.Controllers
{
    public class ProjectSourceTypeController : BaseController
    {
        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            EditProjectSourceTypeModel model = null;
            if (id.HasValue)
            {
                var obj = ProjectSourceTypeLogic.Get(id.Value);
                if (obj != null)
                {
                    model = ModelConverter.ConvertToEdit(obj);
                }
                else
                {
                    this.AddWarning("NoProjectSourceTypeFound", "No ProjectSourceType was found with that Id. Switching to Create mode.");
                    return Redirect(Url.ProjectSourceType_Create());
                }
            }

            if (model == null)
            {
                model = new EditProjectSourceTypeModel();
            }
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditProjectSourceTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.ProjectSourceType_Edit(model.Id));
            }
            ProjectSourceTypeObj obj;
            if (!model.Id.HasValue) 
            {
                obj = ModelConverter.Convert(model);
                obj.Id = ProjectSourceTypeLogic.Create(obj, CurrentUserName);
                if (obj.Id == -1) 
                {
                    this.AddError("CreatingProjectSourceType", "There was an error creating your ProjectSourceType. If this continues contact support.");
                    return Redirect(Url.ProjectSourceType_Create());
                }
            }
            else
            {
                obj = ModelConverter.Convert(model);
                var success = ProjectSourceTypeLogic.Update(obj, CurrentUserName);
                if (!success) 
                {
                    this.AddError("UpdatingProjectSourceType", "There was an error updating your ProjectSourceType. If this continues contact support.");
                    return Redirect(Url.ProjectSourceType_Edit(model.Id.Value));
                }
            }

            return Redirect(Url.ProjectSourceType_Show(obj.Id.Value));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ListProjectSourceTypeModel();
            var objs = ProjectSourceTypeLogic.GetAll();
            model.ProjectSourceTypes = ModelConverter.Convert(objs);
            return View(model);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var obj = ProjectSourceTypeLogic.Get(id);
            if (obj == null) 
            {
                this.AddError("ProjectSourceTypeNotFound", "That ProjectSourceType was not found");
                return Redirect(Url.ProjectSourceType_Index());
            }
            var model = ModelConverter.ConvertToShow(obj);
            return View(model);
        }

        #region -Constructors
        [ExcludeFromCodeCoverage]
        public ProjectSourceTypeController() : this(new InnerTrack.BLL.Logic.ProjectSourceTypeLogic()) {}

        [ExcludeFromCodeCoverage]
        public ProjectSourceTypeController(IProjectSourceTypeLogic logic)
        {
            ProjectSourceTypeLogic = logic;
        }
        #endregion

        #region -Properties
        private IProjectSourceTypeLogic ProjectSourceTypeLogic { get; set; }
        #endregion
    }
}
