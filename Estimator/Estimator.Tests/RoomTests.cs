using Estimator.Data;
using Estimator.Data.Enum;
using Estimator.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Estimator.Data.Exceptions;
using Xunit;
using Estimator = Estimator.Data.Model.Estimator;

namespace Estimator.Tests
{
    public class RoomTests
    {
        #region fields

        private const string titel = "thisIsATitel";
        private const string estimatorName = "Name";
        private const string roomId = "ABCDE";

        private readonly List<DiagramValue> diagramList = new List<DiagramValue>
        {
            new DiagramValue("A", "1"),
            new DiagramValue("B", "2"),
            new DiagramValue("C", "3")
        };

        #endregion

        [Fact]
        public void ConstructorTest()
        {
            #region Assign


            #endregion

            #region Act

            var room = new Room(roomId, new Data.Model.Estimator(estimatorName), RoomType.Fibonacci, 12);

            #endregion

            #region Assert

            Assert.Equal(roomId, room.RoomID);
            Assert.Equal(estimatorName, room.host.Name);
            Assert.Equal(RoomType.Fibonacci, room.Type);

            #endregion
        }

        [Fact]
        public void GetTitelTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.titel = titel;

            #endregion

            #region Act

            var result = room.GetTitel();

            #endregion

            Assert.Equal(result, titel);

            #region Assert

            #endregion
        }

        [Fact]
        public void SetTitelTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);

            #endregion

            #region Act

            room.SetTitel(titel);

            #endregion

            Assert.Equal(room.titel, titel);

            #region Assert

            #endregion
        }

        [Fact]
        public void GetDiagramListTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.diagramDataList = new List<DiagramValue>
            {
                new DiagramValue("A", "1"),
                new DiagramValue("B", "2"),
                new DiagramValue("C", "3")
            };

            #endregion

            #region Act

            var result = room.GetDiagramList();

            #endregion

            Assert.Equal(this.diagramList.Count, result.Count);
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Equal(this.diagramList[i].EstimationCategory, result[i].EstimationCategory);
                Assert.Equal(this.diagramList[i].EstimationCount, result[i].EstimationCount);
            }

            #region Assert

            #endregion
        }

        [Fact]
        public void SetDiagramListTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Tshirt)
            {
                estimators = new List<Data.Model.Estimator>
                {
                    new Data.Model.Estimator("estimator", "L"),
                    new Data.Model.Estimator("estimator1", "L"),
                    new Data.Model.Estimator("estimator2", "XL"),
                    new Data.Model.Estimator("estimator3", "S"),
                }
            };

            #endregion

            #region Act

            room.SetDiagramList();
            var diagramData = room.GetDiagramList();

            var sizeL = diagramData.Where(d => d.EstimationCategory.Equals("L")).ToList();
            var sizeXL = diagramData.Where(d => d.EstimationCategory.Equals("XL")).ToList();
            var sizeS = diagramData.Where(d => d.EstimationCategory.Equals("S")).ToList();

            #endregion

            #region Assert

            Assert.Equal("2", sizeL[0].EstimationCount);
            Assert.Equal("1", sizeXL[0].EstimationCount);
            Assert.Equal("1", sizeS[0].EstimationCount);

            #endregion
        }

        [Fact]
        public void GetEstimatorTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.estimators = new List<Data.Model.Estimator>
            {
                new Data.Model.Estimator(estimatorName)
            };

            #endregion

            #region Act

            var result = room.GetEstimators();

            #endregion

            #region Assert

            Assert.Same(room.estimators, result);

            #endregion
        }

        [Fact]
        public void SetEstimatorTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.estimators.Add(new Data.Model.Estimator(estimatorName));

            #endregion

            #region Act

            room.SetEstimation(new Data.Model.Estimator(estimatorName, "L"));
            var result = room.estimators.Where(e => e.Name.Equals(estimatorName)).ToList();

            #endregion

            #region Assert

            Assert.Equal(estimatorName, result[0].Name);

            #endregion
        }
        
        [Fact]
        public void HasEstimatedTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.estimators.Add(new Data.Model.Estimator(estimatorName));
       
            #endregion

            #region Act

            room.SetEstimation(new Data.Model.Estimator(estimatorName, "3"));
            var result = room.HasEstimated(estimatorName);

            #endregion

            #region Assert

            Assert.True(result);

            #endregion
        }

        [Fact]
        public void HasNotEstimatedTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);
            room.AddEstimator(new Data.Model.Estimator(estimatorName));

            #endregion

            #region Act

            var result = room.HasEstimated(estimatorName);

            #endregion

            #region Assert

            Assert.False(result);

            #endregion
        }

        [Fact]
        public void HasEstimatedExceptionTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);

            #endregion

            #region Act

            var e = Assert.Throws<UsernameNotFoundException>(() =>
                room.HasEstimated(estimatorName));


            #endregion

            #region Assert

            Assert.Equal("Username is not found!", e.Message);


            #endregion
        }
        
        [Fact]
        public void IsAsyncTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci);

            #endregion

            #region Act

            var isAsync = room.IsAsync();


            #endregion

            #region Assert

            Assert.False(isAsync);


            #endregion
        }

        [Fact]
        public void IsNotAsyncTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Model.Estimator("host"), RoomType.Fibonacci, 3);

            #endregion

            #region Act

            var isAsync = room.IsAsync();


            #endregion

            #region Assert

            Assert.True(isAsync);


            #endregion
        }
    }
}