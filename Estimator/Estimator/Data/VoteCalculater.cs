using Estimator.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class VoteCalculater
    {
        private Core core = new Core();

        public void VotingResultRegister(string roomId, string vote, string user)
        {
            var voteList = this.core.RoomManager.RoomList[roomId];
            var votingNumber = 0;
            var hasUserVoted = false;

            foreach (var votingResults in from votingResults in voteList from t in votingResults.Voter.Where(userList => user == userList) select votingResults)
            {
                hasUserVoted = true;
            }

            if (hasUserVoted)
                return;
            
            for (var i = 0; i < voteList.Count; i++)
            {
                if (voteList[i].VotingVariety == vote)
                {
                    votingNumber = i;
                    break;
                }
            }

            voteList[votingNumber].VotingQuantity++;
            voteList[votingNumber].Voter.Add(user);
            this.core.RoomManager.RoomList[roomId] = voteList;
        }

        public List<VotingResults> CloseVoting(string roomId)
        {
            return this.core.RoomManager.RoomList[roomId];
        }
    }
}
