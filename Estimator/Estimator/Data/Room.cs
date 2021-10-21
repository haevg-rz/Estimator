using Estimator.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Estimator.Data
{
    public class Room
    {
        #region fields

        private readonly List<string> fibonacciNumbers = new List<string>()
            {"0", "0.5", "1", "2", "3", "5", "8", "13", "21", "34", "infinite", "coffee"};

        private readonly List<string> tshirtSizes = new List<string>()
            {"XS", "S", "M", "L", "XL", "XXL", "infinite", "coffee"};

        private string RoomID;
        private string titel = string.Empty;
        private int type; // 1 = fibonacinumbers 2 = Tshirt Sizes
        private Estimator host;

        private List<Estimator> estimators = new List<Estimator>();
        private List<DiagramData> diagramDataList;

        #endregion

        #region public

        public Room(string roomId, Estimator host, int type)
        {
            this.RoomID = roomId;
            this.host = host;
            this.type = type;
            this.AddEstimator(host);
        }

        public void AddEstimator(Estimator estimator)
        {
            this.estimators.Add(estimator);
        }

        public void RemoveEstimator(Estimator estimator)
        {
            this.estimators.Remove(estimator);
        }

        public void SetVote(Estimator estimator)
        {
            foreach (var e in this.estimators.Where(e => e.Name == estimator.Name)) e.Vote = estimator.Vote;
        }

        public void ResetAllVotes()
        {
            foreach (var estimator in this.estimators) estimator.Vote = string.Empty;
        }

        public bool IsEstimatorRegistered(string estimatorName)
        {
            return this.estimators.Any(e => e.Name == estimatorName);
        }

        public string GetTitel()
        {
            return this.titel;
        }

        public void SetTitel(string titek)
        {
            this.titel = this.titel;
        }

        public int GetRoomType()
        {
            return this.type;
        }

        public string GetRoomID()
        {
            return this.RoomID;
        }

        //TODO: Überarbeiten!
        public void SetDiagramList(int type)
        {
            var diagrammData = new List<DiagramData>();
            var voteList = this.GetVoteList(type);
            foreach (var voteTopic in voteList)
            {
                var voteNumber = this.estimators.Count(voter => voteTopic == voter.Vote);
                diagrammData.Add(new DiagramData(voteTopic, voteNumber.ToString()));
            }

            this.diagramDataList = diagrammData;
        }

        public List<DiagramData> GetDiagramList()
        {
            return this.diagramDataList;
        }

        #endregion

        #region private

        private List<string> GetVoteList(int type)
        {
            return type == 1 ? this.fibonacciNumbers : this.tshirtSizes;
        }

        #endregion
    }
}