using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace InnerTrack.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class UrlHelperExtension
    {
        #region -Site
        public static string Image(this UrlHelper helper, string name)
        {
            return helper.Content("~/Content/images/" + name);
        }

        public static string StyleSheet(this UrlHelper helper, string name)
        {
            return helper.Content("~/Content/styles/" + name);
        }

        public static string Script(this UrlHelper helper, string name)
        {
            return helper.Content("~/Content/scripts/" + name);
        }
        #endregion

        #region -Projects
        public static string Project_Edit(this UrlHelper helper, int? id)
        {
            return helper.Action("Edit", "Project", new { id = id });
        }
        public static string Project_Create(this UrlHelper helper)
        {
            return helper.Action("Edit", "Project");
        }
        public static string Project_Show(this UrlHelper helper, int id)
        {
            return helper.Action("Show", "Project", new { id = id });
        }
        public static string Project_Index(this UrlHelper helper)
        {
            return helper.Action("Index", "Project");
        }
        #endregion

        #region -Feeds
        public static string Feed_Show(this UrlHelper helper, int id)
        {
            return helper.Action("Show", "Feed", new { id = id });
        }
        public static string Feed_Create(this UrlHelper helper, int projectId)
        {
            return helper.Action("Create", "Feed", new { projectId = projectId });
        }
        public static string Feed_Associate(this UrlHelper helper, int projectId)
        {
            return helper.Action("Associate", "Feed", new { projectId = projectId });
        }
        #endregion
        
        #region -ProjectSourceType
        public static string ProjectSourceType_Edit(this UrlHelper helper, int? id)
        {
            return helper.Action("Edit", "ProjectSourceType", new { id = id });
        }
        public static string ProjectSourceType_Create(this UrlHelper helper)
        {
            return helper.Action("Edit", "ProjectSourceType");
        }
        public static string ProjectSourceType_Show(this UrlHelper helper, int id)
        {
            return helper.Action("Show", "ProjectSourceType", new { id = id });
        }
        public static string ProjectSourceType_Index(this UrlHelper helper)
        {
            return helper.Action("Index", "ProjectSourceType");
        }
        #endregion

        #region -ProjectSource
        public static string ProjectSource_Edit(this UrlHelper helper, int? id)
        {
            return helper.Action("Edit", "ProjectSource", new { id = id });
        }
        public static string ProjectSource_Create(this UrlHelper helper)
        {
            return helper.Action("Edit", "ProjectSource");
        }
        public static string ProjectSource_Show(this UrlHelper helper, int id)
        {
            return helper.Action("Show", "ProjectSource", new { id = id });
        }
        public static string ProjectSource_Index(this UrlHelper helper)
        {
            return helper.Action("Index", "ProjectSource");
        }
        #endregion
    }
}