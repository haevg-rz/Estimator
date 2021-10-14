using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Data.Model
{
    public class DiagramData
    {
        public string VoteTopic { get; set; }
        public string VoteNumber { get; set; }

        public DiagramData(string voteTopic, string voteNumber)
        {
            this.VoteTopic = voteTopic;
            this.VoteNumber = voteNumber;
        }

    }
}
