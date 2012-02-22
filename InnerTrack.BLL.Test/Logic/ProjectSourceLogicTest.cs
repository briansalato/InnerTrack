using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Interfaces.Database;
using Moq;
using InnerTrack.Common.Filters;
using InnerTrack.BLL.Logic;

namespace InnerTrack.BLL.Test.Logic
{
    [TestClass]
    public class ProjectSourceLogicTest
    {
        #region -Get
        [TestMethod]
        public void Get_Id_Found()
        {
            //arrange
            int id = 0;
            var expected = new ProjectSourceObj { Id = id };
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjectSources(It.Is<ProjectSourceFilter>(f => f.Id == id))).Returns(new List<ProjectSourceObj> { expected });
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.Get(id);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Id_Not_Found()
        {
            //arrange
            int id = 0;
            ProjectSourceObj expected = null;
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjectSources(It.Is<ProjectSourceFilter>(f => f.Id == id))).Returns(new List<ProjectSourceObj> { });
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.Get(id);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -Create
        [TestMethod]
        public void Create_Successfully_Made()
        {
            //arrange
            int expected = 4;
            var mockRepository = new Mock<IInnerTrackRepository>();
            var obj = new ProjectSourceObj();
            var user = "test@user.com";
            mockRepository.Setup(m => m.CreateProjectSource(obj, user)).Returns(expected);
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.Create(obj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -Update
        [TestMethod]
        public void Update_Id_Found()
        {
            //arrange
            bool expected = true;
            var mockRepository = new Mock<IInnerTrackRepository>();
            var updatedObj = new ProjectSourceObj { Id = 2 };
            var user = "test@user.com";
            mockRepository.Setup(m => m.UpdateProjectSource(updatedObj, user)).Returns(expected);
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.Update(updatedObj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Update_Id_Not_Found()
        {
            //arrange
            bool expected = false;
            var mockRepository = new Mock<IInnerTrackRepository>();
            var updateObj = new ProjectSourceObj { Id = 2 };
            var user = "test@user.com";
            mockRepository.Setup(m => m.UpdateProjectSource(updateObj, user)).Returns(expected);
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.Update(updateObj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -GetAll
        [TestMethod]
        public void GetAll_No_ProjectSources_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjectSources(It.Is<ProjectSourceFilter>(f => !f.Id.HasValue))).Returns(new List<ProjectSourceObj>());
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.GetAll();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAll_ProjectSources_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new ProjectSourceObj() { Id = 2 };
            mockRepository.Setup(m => m.GetProjectSources(It.Is<ProjectSourceFilter>(f => !f.Id.HasValue))).Returns(new List<ProjectSourceObj>() { expected });
            var logic = new ProjectSourceLogic(mockRepository.Object);

            //act
            var actual = logic.GetAll();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(2, actual[0].Id);
        }
        #endregion
    }
}
