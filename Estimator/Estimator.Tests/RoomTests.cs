using Estimator.Data;
using Estimator.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Estimator = Estimator.Data.Estimator;

namespace Estimator.Tests
{
    public class RoomTests
    {
        #region fields

        private const string titel = "thisIsATitel";
        private const string estimatorName = "Name";
        private const string roomId = "ABCDE";

        private readonly List<DiagramData> diagramList = new List<DiagramData>
        {
            new DiagramData("A", "1"),
            new DiagramData("B", "2"),
            new DiagramData("C", "3")
        };

        #endregion

        [Fact]
        public void GetTitelTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Estimator("host"), 1);
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

            var room = new Room(roomId, new Data.Estimator("host"), 1);

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

            var room = new Room(roomId, new Data.Estimator("host"), 1);
            room.diagramDataList = new List<DiagramData>
            {
                new DiagramData("A", "1"),
                new DiagramData("B", "2"),
                new DiagramData("C", "3")
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

            var room = new Room(roomId, new Data.Estimator("host"), 2)
            {
                estimators = new List<Data.Estimator>
                {
                    new Data.Estimator("estimator", "L"),
                    new Data.Estimator("estimator1", "L"),
                    new Data.Estimator("estimator2", "XL"),
                    new Data.Estimator("estimator3", "S"),
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

            Assert.Equal(sizeL[0].EstimationCount, "2");
            Assert.Equal(sizeXL[0].EstimationCount, "1");
            Assert.Equal(sizeS[0].EstimationCount, "1");

            #endregion
        }

        [Fact]
        public void GetEstimatorTest()
        {
            #region Assign

            var room = new Room(roomId, new Data.Estimator("host"), 1);
            room.estimators = new List<Data.Estimator>
            {
                new Data.Estimator(estimatorName)
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

            var room = new Room(roomId, new Data.Estimator("host"), 1);
            room.estimators.Add(new Data.Estimator(estimatorName));

            #endregion

            #region Act

            room.SetEstimation(new Data.Estimator(estimatorName, "L"));
            var result = room.estimators.Where(e => e.Name.Equals(estimatorName)).ToList();

            #endregion

            #region Assert

            Assert.Equal(estimatorName, result[0].Name);

            #endregion
        }
    }
}