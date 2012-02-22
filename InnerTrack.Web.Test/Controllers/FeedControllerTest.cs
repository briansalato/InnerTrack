using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using System.Web.Mvc;
using Moq;
using InnerTrack.Web.Controllers;
using InnerTrack.Web.Models;
using InnerTrack.Web.Helpers;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.Web.Test.Controllers
{
    [TestClass]
    public class FeedControllerTest : BaseControllerTest
    {
        #region -Show
        [TestMethod]
        public void Show_Id_Found()
        {
            //arrange
            var mockLogic = new Mock<IFeedLogic>();
            var id = 2;
            var obj = new FeedObj()
            {
                Id = id,
                Events = new List<EventObj>(),
                Type = new FeedTypeObj() { Id = 0 }
            };
            mockLogic.Setup(l => l.Get(id)).Returns(obj);
            var controller = new FeedController(mockLogic.Object, null);

            //act
            var actual = controller.Show(id) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as ShowFeedModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Show_Id_Not_Found()
        {
            //arrange
            var mockLogic = new Mock<IFeedLogic>();
            var id = 2;
            FeedObj obj = null;
            mockLogic.Setup(l => l.Get(id)).Returns(obj);
            var controller = new FeedController(mockLogic.Object, null);
            controller = SetupController(controller);

            //act
            var actual = controller.Show(id) as RedirectResult;
            var expectedUrl = controller.Url.Project_Index();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion

        #region -Create Get
        [TestMethod]
        public void Create_Project_Found()
        {
            //arrange
            var projectId = 2;
            var project = new ProjectObj() { Id = projectId };
            var mockProjectLogic = new Mock<IProjectLogic>();
            mockProjectLogic.Setup(l => l.Get(projectId)).Returns(project);
            var mockFeedLogic = new Mock<IFeedLogic>();
            mockFeedLogic.Setup(l => l.GetFeedTypes()).Returns(new List<FeedType>() { FeedType.News });
            var controller = new FeedController(mockFeedLogic.Object, mockProjectLogic.Object);

            //act
            var actual = controller.Create(projectId) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as EditFeedModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.AvailableTypes.Count());
            Assert.AreEqual(((int)FeedType.News).ToString(), model.AvailableTypes.First().Value);
        }

        [TestMethod]
        public void Create_Project_Not_Found()
        {
            //arrange
            var projectId = 2;
            ProjectObj project = null;
            var mockProjectLogic = new Mock<IProjectLogic>();
            mockProjectLogic.Setup(l => l.Get(projectId)).Returns(project);
            var mockFeedLogic = new Mock<IFeedLogic>();
            mockFeedLogic.Setup(l => l.GetFeedTypes()).Returns(new List<FeedType>());
            var controller = new FeedController(mockFeedLogic.Object, mockProjectLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Create(projectId) as RedirectResult;
            var expectedUrl = controller.Url.Project_Index();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion

        #region -Create Post
        [TestMethod]
        public void Create_Post_Bad_ModelState()
        {
            //arrange
            var mockLogic = new Mock<IFeedLogic>(MockBehavior.Strict);
            var projectId = 2;
            var model = new EditFeedModel() { ProjectId = projectId };
            var controller = new FeedController(mockLogic.Object, null);
            controller = SetupController(controller);
            controller.ModelState.AddModelError("test", "test");

            //act
            var actual = controller.Create(model) as RedirectResult;
            var expectedUrl = controller.Url.Feed_Create(projectId);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Create_Post_Save_Fails()
        {
            //arrange
            var mockLogic = new Mock<IFeedLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<FeedObj>(), It.IsAny<string>())).Returns(-1);
            var projectId = 2;
            var model = new EditFeedModel() { ProjectId = projectId };
            var controller = new FeedController(mockLogic.Object, null);
            controller = SetupController(controller);

            //act
            var actual = controller.Create(model) as RedirectResult;
            var expectedUrl = controller.Url.Feed_Create(projectId);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Create_Post_Save_Success()
        {
            //arrange
            var mockLogic = new Mock<IFeedLogic>(MockBehavior.Strict);
            var newId = 3;
            mockLogic.Setup(m => m.Create(It.IsAny<FeedObj>(), It.IsAny<string>())).Returns(newId);
            var model = new EditFeedModel() { ProjectId = 2 };
            var controller = new FeedController(mockLogic.Object, null);
            controller = SetupController(controller);

            //act
            var actual = controller.Create(model) as RedirectResult;
            var expectedUrl = controller.Url.Feed_Show(newId);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion
    }
}
