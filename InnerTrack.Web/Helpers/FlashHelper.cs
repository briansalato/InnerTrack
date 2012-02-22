using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace InnerTrack.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    internal static partial class FlashHelper
    {
        internal static void AddWarning(this ControllerBase controller, string key, string message)
        {
            AddFlash(controller, "warnings", key, message);
        }

        internal static IDictionary<string, MvcHtmlString> GetWarnings(this HtmlHelper helper)
        {
            return GetFlashMessages(helper, "warnings");
        }

        internal static void AddError(this ControllerBase controller, string key, string message)
        {
            AddFlash(controller, "errors", key, message);
        }

        internal static IDictionary<string, MvcHtmlString> GetErrors(this HtmlHelper helper)
        {
            return GetFlashMessages(helper, "errors");
        }

        private static void AddFlash(ControllerBase controller, string sessionKey, string key, string message)
        {
            sessionKey = "Flash." + sessionKey;
            IDictionary<string, MvcHtmlString> messages = controller.TempData[sessionKey] as Dictionary<string, MvcHtmlString>;
            if (messages == null)
            {
                messages = new Dictionary<string, MvcHtmlString>();
            }
            if (messages.ContainsKey(key))
            {
                messages.Remove(key);
            }
            messages.Add(key, new MvcHtmlString(message.Replace("\n", "<br />")));
            controller.TempData[sessionKey] = messages;
        }

        private static IDictionary<string, MvcHtmlString> GetFlashMessages(HtmlHelper helper, string sessionKey)
        {
            sessionKey = "Flash." + sessionKey;
            return (helper.ViewContext.TempData[sessionKey] != null
                        ? (IDictionary<string, MvcHtmlString>)helper.ViewContext.TempData[sessionKey]
                        : null);
        }
    }
}