using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerTrack.Common.Interfaces.Database;
using Moq;
using InnerTrack.Common.Filters;
using InnerTrack.Common.Objs;
using InnerTrack.BLL.Logic;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.BLL.Test.Logic
{
    [TestClass]
    public class FeedLogicTest
    {
        #region -Get
        [TestMethod]
        public void Get_Id_Found()
        {
            //arrange
            int id = 0;
            var expected = new FeedObj { Id = id };
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetFeeds(It.Is<FeedFilter>(f => f.Id == id))).Returns(new List<FeedObj> { expected });
            var logic = new FeedLogic(mockRepository.Object);

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
            FeedObj expected = null;
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetFeeds(It.Is<FeedFilter>(f => f.Id == id))).Returns(new List<FeedObj> { });
            var logic = new FeedLogic(mockRepository.Object);

            //act
            var actual = logic.Get(id);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -GetForProduct
        [TestMethod]
        public void GetForProduct_No_Feeds_For_Project()
        {
            //arrange
            int projectId = 0;
            FeedObj expected = new FeedObj { Id = 7 };
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetFeeds(It.Is<FeedFilter>(f => f.ProjectId == projectId))).Returns(new List<FeedObj> {  });
            var logic = new FeedLogic(mockRepository.Object);

            //act
            var actual = logic.GetForProject(projectId);

            //assert
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetForProduct_Feeds_Found_For_Project()
        {
            //arrange
            int projectId = 0;
            FeedObj expected = new FeedObj { Id = 7 };
            var mockRepository = new Mock<IInnerTrackRepository>();
            mockRepository.Setup(m => m.GetFeeds(It.Is<FeedFilter>(f => f.ProjectId == projectId))).Returns(new List<FeedObj> { expected });
            var logic = new FeedLogic(mockRepository.Object);

            //act
            var actual = logic.GetForProject(projectId);

            //assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected.Id, actual[0].Id);
        }
        #endregion

        #region -Create
        [TestMethod]
        public void Create_Successfully_Made()
        {
            //arrange
            int expected = 4;
            var mockRepository = new Mock<IInnerTrackRepository>();
            var obj = new FeedObj();
            var user = "test@user.com";
            mockRepository.Setup(m => m.CreateFeed(obj, user)).Returns(expected);
            var logic = new FeedLogic(mockRepository.Object);

            //act
            var actual = logic.Create(obj, user);

            //assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region -GetFeedTypes
        [TestMethod]
        public void GetFeedTypes_Test()
        {
            //arrange
            var logic = new FeedLogic();

            //act
            var results = logic.GetFeedTypes();

            //assert
            Assert.IsTrue(results.Contains(FeedType.News));
            Assert.IsTrue(results.Contains(FeedType.SVN));
        }
        #endregion
    }
}
