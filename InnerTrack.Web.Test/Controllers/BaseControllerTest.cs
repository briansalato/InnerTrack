using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Security.Principal;

namespace InnerTrack.Web.Test.Controllers
{
    public class BaseControllerTest
    {
        public T SetupController<T>(T controller) where T : Controller
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(r => r.ApplicationPath).Returns("/");
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());
            var queryString = new System.Collections.Specialized.NameValueCollection();
            request.SetupGet(x => x.QueryString).Returns(queryString);

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string s) => "http://localhost/" + s);

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            var userMock = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.Name).Returns("brian.salato");
            userMock.SetupGet(p => p.Identity).Returns(identity.Object);
            context.SetupGet(x => x.User).Returns(userMock.Object);



            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);
            return controller;
        }
    }
}
