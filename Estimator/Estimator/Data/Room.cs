using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class Room
    {
        private string roomName;
        private string type;

        private List<Voter> voter;

        public Room(string name, string type)
        {
            this.roomName = name;
            this.type = type;
            this.voter = new List<Voter>();
        }

        public void AddVoter(Voter voter)
        {
            this.voter.Add(voter);
        }

        public void AddVote(Voter voter)
        {
            var voterPlace = 0;

            for (var i = 0; i < this.voter.Count; i++)
            {
                if (this.voter[i].Name == voter.Name)
                    voterPlace = i;
            }

            this.voter[voterPlace].Vote = voter.Vote;
        }

        public string GetType()
        {
            return this.type;
        }

        public bool HasVoterJoin(string newVoter)
        {
            return this.voter.Any(t => t.Name == newVoter);
        }

        public void ResetAllVotes()
        {
            foreach (var voter in this.voter)
            {
                voter.Vote = null;
            }
        }
    }
}
