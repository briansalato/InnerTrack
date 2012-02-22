using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerTrack.Web.Controllers;
using System.Web.Mvc;
using InnerTrack.Web.Models;
using InnerTrack.Common.Interfaces.Logic;
using Moq;
using InnerTrack.Common.Objs;
using InnerTrack.Web.Helpers;

namespace InnerTrack.Web.Test.Controllers
{
    [TestClass]
    public class ProjectControllerTest : BaseControllerTest
    {
        #region -Edit Get
        [TestMethod]
        public void Edit_Create_New()
        {
            //arrange
            var controller = new ProjectController();

            //act
            var actual = controller.Edit((int?)null) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as EditProjectModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Edit_Update_Id_Found()
        {
            //arrange
            var id = 0;
            var mockLogic = new Mock<IProjectLogic>();
            var obj = new ProjectObj { Id = id };
            mockLogic.Setup(l => l.Get(id)).Returns(obj);
            var controller = new ProjectController(mockLogic.Object);

            //act
            var actual = controller.Edit(0) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as EditProjectModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Edit_Update_Id_Not_Found()
        {
            //arrange
            var id = 0;
            var mockLogic = new Mock<IProjectLogic>();
            mockLogic.Setup(l => l.Get(id)).Returns((ProjectObj)null);
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController<ProjectController>(controller);

            //act
            var actual = controller.Edit(0) as RedirectResult;
            var expectedUrl = controller.Url.Project_Create();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion

        #region -Edit Post
        [TestMethod]
        public void Edit_Post_Create_Bad_ModelState()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            var model = new EditProjectModel();
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);
            controller.ModelState.AddModelError("test", "test");

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Create();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Edit_Post_Update_Bad_ModelState()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            var model = new EditProjectModel() { Id = 0 };
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);
            controller.ModelState.AddModelError("test", "test");

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Edit(0);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        
        [TestMethod]
        public void Edit_Post_Create_Success()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<ProjectObj>(), It.IsAny<string>())).Returns(2);
            var model = new EditProjectModel();
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Show(2);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Edit_Post_Create_Failure()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<ProjectObj>(), It.IsAny<string>())).Returns(-1);
            var model = new EditProjectModel();
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Create();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Edit_Post_Update_Success()
        {
            //arrange
            var id = 2;
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Update(It.Is<ProjectObj>(p => p.Id == id), It.IsAny<string>())).Returns(true);
            var model = new EditProjectModel() { Id = id };
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Show(id);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }

        [TestMethod]
        public void Edit_Post_Update_Failure()
        {
            //arrange
            var id = 2;
            var mockLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Update(It.Is<ProjectObj>(p => p.Id == id), It.IsAny<string>())).Returns(false);
            var model = new EditProjectModel() { Id = id };
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Edit(model) as RedirectResult;
            var expectedUrl = controller.Url.Project_Edit(id);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion

        #region -Index
        [TestMethod]
        public void Index_No_Projects()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var items = new List<ProjectObj>();
            mockLogic.Setup(l => l.GetAll()).Returns(items);
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as ListProjectModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Projects);
        }

        [TestMethod]
        public void Index_Projects_Found()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var items = new List<ProjectObj>
            {
                new ProjectObj() { Id = 2 }
            };
            mockLogic.Setup(l => l.GetByOwner("brian.salato")).Returns(items);
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as ListProjectModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Projects);
            Assert.AreEqual(1, model.Projects.Count);
            Assert.AreEqual(2, model.Projects[0].Id);
        }
        #endregion

        #region -Show
        [TestMethod]
        public void Show_Id_Found()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var id = 2;
            var obj = new ProjectObj() 
            { 
                Id = id,
                Feeds = new List<FeedObj>()
            };
            mockLogic.Setup(l => l.Get(id)).Returns(obj);
            var controller = new ProjectController(mockLogic.Object);

            //act
            var actual = controller.Show(id) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as ShowProjectModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Show_Id_Not_Found()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var id = 2;
            ProjectObj obj = null;
            mockLogic.Setup(l => l.Get(id)).Returns(obj);
            var controller = new ProjectController(mockLogic.Object);
            controller = SetupController(controller);

            //act
            var actual = controller.Show(id) as RedirectResult;
            var expectedUrl = controller.Url.Project_Index();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedUrl, actual.Url);
        }
        #endregion

        #region -BasicSearch
        [TestMethod]
        public void BasicSearch_Default_Parameters()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var obj = new SearchResult<ProjectObj>
            {
                HasMore = true,
                HasPrevious = false,
                Results = new List<ProjectObj>()
                {
                    new ProjectObj()
                    {
                        Id = 2
                    }
                }
            };
            mockLogic.Setup(l => l.Search("query", 0, 20)).Returns(obj);
            var controller = new ProjectController(mockLogic.Object);

            //act
            var actual = controller.BasicSearch("query") as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as BasicSearchModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.CurrentPage);
            Assert.AreEqual(true, model.HasMoreResults);
            Assert.AreEqual(false, model.HasPriorResults);
            Assert.AreEqual("Project", model.ModelName);
            Assert.AreEqual(20, model.PageSize);
            Assert.AreEqual("query", model.Query);
            Assert.AreEqual(1, model.Results.Count);
            Assert.AreEqual(2, model.Results[0].Id);
        }
        
        [TestMethod]
        public void BasicSearch_NonDefault_Parameters()
        {
            //arrange
            var mockLogic = new Mock<IProjectLogic>();
            var obj = new SearchResult<ProjectObj>
            {
                HasMore = false,
                HasPrevious = true,
                Results = new List<ProjectObj>()
                {
                    new ProjectObj()
                    {
                        Id = 2
                    }
                }
            };
            mockLogic.Setup(l => l.Search("query", 5, 10)).Returns(obj);
            var controller = new ProjectController(mockLogic.Object);

            //act
            var actual = controller.BasicSearch("query", 5, 10) as ViewResult;

            //assert
            Assert.IsNotNull(actual);
            var model = actual.Model as BasicSearchModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(5, model.CurrentPage);
            Assert.AreEqual(false, model.HasMoreResults);
            Assert.AreEqual(true, model.HasPriorResults);
            Assert.AreEqual("Project", model.ModelName);
            Assert.AreEqual(10, model.PageSize);
            Assert.AreEqual("query", model.Query);
            Assert.AreEqual(1, model.Results.Count);
            Assert.AreEqual(2, model.Results[0].Id);
        }
        #endregion
    }
}
