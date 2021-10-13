using Estimator.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class User
    {
        public int AddVoter(string roomId, string vote, string user, List<VotingResults> voteList)
        {
            if (this.HasUserVoted(voteList))
                return 0;

            return VotingNumber(voteList,vote);
        }
        private bool HasUserVoted(List<VotingResults> voteList)
        {
            var hasUserVoted = false;

            foreach (var votingResults in from votingResults in voteList from t in votingResults.Voter.Where(userList => user == userList) select votingResults)
            {
                hasUserVoted = true;
            }

            return hasUserVoted;
        }
        private int VotingNumber(List<VotingResults> voteList, string vote)
        {
            var votingNumber = 0;
            for (var i = 0; i < voteList.Count; i++)
            {
                if (voteList[i].VotingVariety == vote)
                {
                    votingNumber = i;
                    break;
                }
            }

            return votingNumber;
        }
    }
}
