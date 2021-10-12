using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimator.Data.Model;

namespace Estimator.Data
{
    public class VoteCalculater
    {
        private Core core = new Core();

        public void VotingResultRegister(string roomId, string vote)
        {
            var voteList = this.core.RoomManager.RoomList[roomId];
            var votingNumber = 0;
            for (var i = 0; i < voteList.Count; i++)
            {
                if (voteList[i].VotingVariety == vote)
                {
                    votingNumber = i;
                    break;
                }

            }

            voteList[votingNumber].VotingQuantity++;
            this.core.RoomManager.RoomList[roomId] = voteList;
        }

        public List<VotingResults> CloseVoting(string roomId)
        {
            return this.core.RoomManager.RoomList[roomId];
        }
    }
}
