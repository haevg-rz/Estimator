using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class Voter
    {
        public string Name { get; set; }
        public string Vote { get; set; }

        public Voter(string name, string vote)
        {
            this.Name = name;
            this.Vote = vote;
        }

    }
}
