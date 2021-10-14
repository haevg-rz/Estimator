using System;
using System.Collections.Generic;
using System.Linq;
using Estimator.Data.Model;

namespace Estimator.Data
{
    public class Room
    {
        #region fields

        private readonly List<string> fibunacciNumbers = new List<string>(){"0","0.5","1","2","3","5","8","13","21","34","infinite","coffee"};
        private readonly List<string> tshirtSizes = new List<string>(){"XS","S","M","L","XL","XXL","infinite","coffee"};

        private string taskName;
        private int type; // 1 = fibunacinumbers 2 = Tshirt Sizes

        private List<Voter> voter;
        private List<DiagramData> diagrammDataList;

        #endregion

        #region public

        public Room(string name, int type)
        {
            this.taskName = name;
            this.type = type;
            this.voter = new List<Voter>();
        }

        public void AddVoter(Voter voter)
        {
            this.voter.Add(voter);
        }

        public void RemoveVoter(Voter voter)
        {
            this.voter.RemoveAt(this.GetVoter(voter));
        }

        public void SetVote(Voter voter)
        {
            this.voter[this.GetVoter(voter)].Vote = voter.Vote;
        }

        public void ResetAllVotes()
        {
            foreach (var voter in this.voter)
            {
                voter.Vote = String.Empty;
            }
        }

        public bool IsVoterJoin(string newVoter)
        {
            return this.voter.Any(t => t.Name == newVoter);
        }

        public string GetTaskName()
        {
            return this.taskName;
        }

        public void SetTaskName(string taskname)
        {
            this.taskName = taskname;
        }

        public int GetType()
        {
            return this.type;
        }

        public void SetDiagramList(int type)
        {
            var diagrammData = new List<DiagramData>();
            var voteList = GetVoteList(type);
            foreach (var voteTopic in voteList)
            {
                var voteNumber = this.voter.Count(voter => voteTopic == voter.Vote);
                diagrammData.Add(new DiagramData(voteTopic,voteNumber.ToString()));
            }

            this.diagrammDataList = diagrammData;
        }

        public List<DiagramData> GetDiagramList()
        {
            return this.diagrammDataList;
        }

        #endregion

        #region private

        private List<string> GetVoteList(int type)
        {
            return type == 1 ? this.fibunacciNumbers : this.tshirtSizes;
        }

        private int GetVoter(Voter voter)
        {
            var voterNumber = 0;

            for (var i = 0; i < this.voter.Count; i++)
            {
                if (this.voter[i].Name == voter.Name)
                {
                    voterNumber = i;
                    break;
                }
            }

            return voterNumber;

        }

        #endregion
    }
}
