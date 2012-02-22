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
    public class ProjectSourceController : BaseController
    {
        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            EditProjectSourceModel model = null;
            int? currentConfigTypeId = null;
            if (id.HasValue)
            {
                var obj = ProjectSourceLogic.Get(id.Value);
                if (obj != null)
                {
                    model = ModelConverter.ConvertToEdit(obj);
                    currentConfigTypeId = obj.SourceConfig.Id;
                }
                else
                {
                    this.AddWarning("NoProjectSourceFound", "No ProjectSource was found with that Id. Switching to Create mode.");
                    return Redirect(Url.ProjectSource_Create());
                }
            }

            if (model == null)
            {
                model = new EditProjectSourceModel();
            }

            var availableTypeItems = ProjectSourceTypeLogic.GetAll()
                                                            .Select(c => new SelectListItem { 
                                                                                Text = c.Name, 
                                                                                Value = c.Id.ToString() 
                                                                            });
            model.AvailableTypes = new SelectList(availableTypeItems, availableTypeItems.FirstOrDefault(a => a.Value == currentConfigTypeId.ToString()));

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditProjectSourceModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.ProjectSource_Edit(model.Id));
            }
            ProjectSourceObj obj;
            if (!model.Id.HasValue) 
            {
                obj = ModelConverter.Convert(model);
                obj.Id = ProjectSourceLogic.Create(obj, CurrentUserName);
                if (obj.Id == -1) 
                {
                    this.AddError("CreatingProjectSource", "There was an error creating your ProjectSource. If this continues contact support.");
                    return Redirect(Url.ProjectSource_Create());
                }
            }
            else
            {
                obj = ModelConverter.Convert(model);
                var success = ProjectSourceLogic.Update(obj, CurrentUserName);
                if (!success) 
                {
                    this.AddError("UpdatingProjectSource", "There was an error updating your ProjectSource. If this continues contact support.");
                    return Redirect(Url.ProjectSource_Edit(model.Id.Value));
                }
            }

            return Redirect(Url.ProjectSource_Show(obj.Id.Value));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ListProjectSourceModel();
            var objs = ProjectSourceLogic.GetAll();
            model.ProjectSources = ModelConverter.Convert(objs);
            return View(model);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var obj = ProjectSourceLogic.Get(id);
            if (obj == null) 
            {
                this.AddError("ProjectSourceNotFound", "That ProjectSource was not found");
                return Redirect(Url.ProjectSource_Index());
            }
            var model = ModelConverter.ConvertToShow(obj);
            return View(model);
        }

        #region -Constructors
        [ExcludeFromCodeCoverage]
        public ProjectSourceController() : this(new InnerTrack.BLL.Logic.ProjectSourceLogic(),
                                                new InnerTrack.BLL.Logic.ProjectSourceTypeLogic()) {}

        [ExcludeFromCodeCoverage]
        public ProjectSourceController(IProjectSourceLogic logic, IProjectSourceTypeLogic typeLogic)
        {
            ProjectSourceLogic = logic;
            ProjectSourceTypeLogic = typeLogic;
        }
        #endregion

        #region -Properties
        private IProjectSourceLogic ProjectSourceLogic { get; set; }
        private IProjectSourceTypeLogic ProjectSourceTypeLogic { get; set; }
        #endregion
    }
}
