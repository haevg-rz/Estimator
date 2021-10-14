using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class Voter
    {
        private string name;
        private string vote;

        public Voter(string name, string vote)
        {
            this.name = name;
            this.vote = vote;
        }

        public bool HasUserVoted(string user, List<Voter> userList)
        {
            return userList.Any(voter => voter.name == user);
        }

    }
}
