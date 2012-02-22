using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerTrack.BLL.Logic;
using InnerTrack.Common.Objs;
using Moq;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Filters;

namespace InnerTrack.BLL.Test.Logic
{
    [TestClass]
    public class ProjectLogicTest
    {
        #region -Get
        [TestMethod]
        public void Get_Id_Found()
        {
            //arrange
            int id = 0;
            var expected = new ProjectObj { Id = id };
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.Id == id))).Returns(new List<ProjectObj> { expected });
            var logic = new ProjectLogic(mockRepository.Object);

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
            ProjectObj expected = null;
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.Id == id))).Returns(new List<ProjectObj> { });
            var logic = new ProjectLogic(mockRepository.Object);

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
            var obj = new ProjectObj();
            var user = "test@user.com";
            mockRepository.Setup(m => m.CreateProject(obj, user)).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

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
            var updateProj = new ProjectObj { Id = 2 };
            var user = "test@user.com";
            mockRepository.Setup(m => m.UpdateProject(updateProj, user)).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Update(updateProj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Update_Id_Not_Found()
        {
            //arrange
            bool expected = false;
            var mockRepository = new Mock<IInnerTrackRepository>();
            var updateProj = new ProjectObj { Id = 2 };
            var user = "test@user.com";
            mockRepository.Setup(m => m.UpdateProject(updateProj, user)).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Update(updateProj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -GetAll
        [TestMethod]
        public void GetAll_No_Projects_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => !f.Id.HasValue))).Returns(new List<ProjectObj>());
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.GetAll();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAll_Projects_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new ProjectObj() { Id = 2 };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => !f.Id.HasValue))).Returns(new List<ProjectObj>() { expected });
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.GetAll();

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(2, actual[0].Id);
        }
        #endregion

        #region -GetByOwner
        [TestMethod]
        public void GetByOwner_No_Projects_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var username = "brian.salato";
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.OwnersUserName == username))).Returns(new List<ProjectObj>());
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.GetByOwner(username);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetByOwner_Projects_Found()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var username = "brian.salato";
            var expected = new ProjectObj() { Id = 2 };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.OwnersUserName == username))).Returns(new List<ProjectObj>() { expected });
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.GetByOwner(username);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(2, actual[0].Id);
        }
        #endregion

        #region -Search
        [TestMethod]
        public void Search_No_Results()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.MaxResults == 3
                                                                              && f.StartIndex == 0
                                                                              && f.QueryNames.Count == 2
                                                                              && f.QueryNames.Contains("dog")
                                                                              && f.QueryNames.Contains("cat")
                                                                              && f.QueryDescriptions.Count == 2
                                                                              && f.QueryDescriptions.Contains("dog")
                                                                              && f.QueryDescriptions.Contains("cat")))).Returns(new List<ProjectObj>());
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Search("dog cat", 0, 2);

            //assert
            Assert.AreEqual(0, actual.Results.Count);
            Assert.IsFalse(actual.HasPrevious);
            Assert.IsFalse(actual.HasMore);
        }

        [TestMethod]
        public void Search_First_Page()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new List<ProjectObj>()
            {
                new ProjectObj() { Id = 2 },
                new ProjectObj() { Id = 4 },
                new ProjectObj() { Id = 6 }
            };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.MaxResults == 3
                                                                              && f.StartIndex == 0
                                                                              && f.QueryNames.Count == 2
                                                                              && f.QueryNames.Contains("dog")
                                                                              && f.QueryNames.Contains("cat")
                                                                              && f.QueryDescriptions.Count == 2
                                                                              && f.QueryDescriptions.Contains("dog")
                                                                              && f.QueryDescriptions.Contains("cat")))).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Search("dog cat", 0, 2);

            //assert
            Assert.AreEqual(2, actual.Results.Count);
            Assert.AreEqual(expected[0].Id, actual.Results[0].Id);
            Assert.AreEqual(expected[1].Id, actual.Results[1].Id);
            Assert.IsFalse(actual.HasPrevious);
            Assert.IsTrue(actual.HasMore);
        }

        [TestMethod]
        public void Search_Middle_Page()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new List<ProjectObj>()
            {
                new ProjectObj() { Id = 6 },
                new ProjectObj() { Id = 8 },
                new ProjectObj() { Id = 10 }
            };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.MaxResults == 3
                                                                              && f.StartIndex == 2
                                                                              && f.QueryNames.Count == 2
                                                                              && f.QueryNames.Contains("dog")
                                                                              && f.QueryNames.Contains("cat")
                                                                              && f.QueryDescriptions.Count == 2
                                                                              && f.QueryDescriptions.Contains("dog")
                                                                              && f.QueryDescriptions.Contains("cat")))).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Search("dog cat", 1, 2);

            //assert
            Assert.AreEqual(2, actual.Results.Count);
            Assert.AreEqual(expected[0].Id, actual.Results[0].Id);
            Assert.AreEqual(expected[1].Id, actual.Results[1].Id);
            Assert.IsTrue(actual.HasPrevious);
            Assert.IsTrue(actual.HasMore);
        }

        [TestMethod]
        public void Search_Partial_Full_Last_Page()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new List<ProjectObj>()
            {
                new ProjectObj() { Id = 10 }
            };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.MaxResults == 3
                                                                              && f.StartIndex == 4
                                                                              && f.QueryNames.Count == 2
                                                                              && f.QueryNames.Contains("dog")
                                                                              && f.QueryNames.Contains("cat")
                                                                              && f.QueryDescriptions.Count == 2
                                                                              && f.QueryDescriptions.Contains("dog")
                                                                              && f.QueryDescriptions.Contains("cat")))).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Search("dog cat", 2, 2);

            //assert
            Assert.AreEqual(1, actual.Results.Count);
            Assert.AreEqual(expected[0].Id, actual.Results[0].Id);
            Assert.IsTrue(actual.HasPrevious);
            Assert.IsFalse(actual.HasMore);
        }

        [TestMethod]
        public void Search_Full_Last_Page()
        {
            //arrange
            var mockRepository = new Mock<IInnerTrackRepository>();
            var expected = new List<ProjectObj>()
            {
                new ProjectObj() { Id = 10 },
                new ProjectObj() { Id = 11 }
            };
            mockRepository.Setup(m => m.GetProjects(It.Is<ProjectFilter>(f => f.MaxResults == 3
                                                                              && f.StartIndex == 4
                                                                              && f.QueryNames.Count == 2
                                                                              && f.QueryNames.Contains("dog")
                                                                              && f.QueryNames.Contains("cat")
                                                                              && f.QueryDescriptions.Count == 2
                                                                              && f.QueryDescriptions.Contains("dog")
                                                                              && f.QueryDescriptions.Contains("cat")))).Returns(expected);
            var logic = new ProjectLogic(mockRepository.Object);

            //act
            var actual = logic.Search("dog cat", 2, 2);

            //assert
            Assert.AreEqual(2, actual.Results.Count);
            Assert.AreEqual(expected[0].Id, actual.Results[0].Id);
            Assert.AreEqual(expected[1].Id, actual.Results[1].Id);
            Assert.IsTrue(actual.HasPrevious);
            Assert.IsFalse(actual.HasMore);
        }
        #endregion
    }
}
