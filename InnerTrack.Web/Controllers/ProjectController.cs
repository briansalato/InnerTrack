using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Web.Helpers;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Web.Models;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.Common.Objs;

namespace InnerTrack.Web.Controllers
{
    public class ProjectController : BaseController
    {
        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            EditProjectModel model = null;
            if (id.HasValue)
            {
                var project = ProjectLogic.Get(id.Value);
                if (project != null)
                {
                    model = ModelConverter.ConvertToEdit(project);
                }
                else
                {
                    this.AddWarning("NoItemFound", "No project was found with that Id. Switching to Create mode.");
                    return Redirect(Url.Project_Create());
                }
            }

            if (model == null)
            {
                model = new EditProjectModel();
                model.Tags = new List<ListItemTagModel>();
            }
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Project_Edit(model.Id));
            }
            ProjectObj project;
            if (!model.Id.HasValue) 
            {
                project = ModelConverter.Convert(model);
                project.Id = ProjectLogic.Create(project, CurrentUserName);
                if (project.Id == -1) 
                {
                    this.AddError("CreatingProject", "There was an error creating your project. If this continues contact support.");
                    return Redirect(Url.Project_Create());
                }
            }
            else
            {
                project = ModelConverter.Convert(model);
                var success = ProjectLogic.Update(project, CurrentUserName);
                if (!success) 
                {
                    this.AddError("UpdatingProject", "There was an error updating your project. If this continues contact support.");
                    return Redirect(Url.Project_Edit(model.Id.Value));
                }
            }

            return Redirect(Url.Project_Show(project.Id.Value));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ListProjectModel();
            var objs = ProjectLogic.GetByOwner(CurrentUserName);
            model.Projects = ModelConverter.Convert(objs);
            return View(model);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var obj = ProjectLogic.Get(id);
            if (obj == null) 
            {
                this.AddError("ProjectNotFound", "That project was not found");
                return Redirect(Url.Project_Index());
            }
            var model = ModelConverter.ConvertToShow(obj);
            return View(model);
        }

        [HttpGet]
        public ActionResult BasicSearch(string query, int? currentPage = null, int? pageSize = null)
        {
            currentPage = currentPage ?? 0;
            pageSize = pageSize ?? 20;

            var searchResult = ProjectLogic.Search(query, currentPage.Value, pageSize.Value);
            var results = ModelConverter.ConvertToSearch(searchResult.Results);

            var model = new BasicSearchModel
            {
                CurrentPage = currentPage.Value,
                PageSize = pageSize.Value,
                Query = query,
                ModelName = "Project",
                HasMoreResults = searchResult.HasMore,
                HasPriorResults = searchResult.HasPrevious,
                Results = results
            };

            return View("BasicSearch", model);
        }

        #region -Constructors
        [ExcludeFromCodeCoverage]
        public ProjectController() : this(new BLL.Logic.ProjectLogic()) {}

        [ExcludeFromCodeCoverage]
        public ProjectController(IProjectLogic projectLogic)
        {
            ProjectLogic = projectLogic;
        }
        #endregion

        #region -Properties
        private IProjectLogic ProjectLogic { get; set; }
        #endregion
    }
}
