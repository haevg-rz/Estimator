using System;
using System.Collections.Generic;

namespace Estimator.Data.Model
{
    public class VotingResults
    {
        public string VotingVariety { get; set; }
        public List<string> Voter { get; set; }

        public VotingResults(string votingVariety, List<string> voter)
        {
            this.VotingVariety = votingVariety;
            this.Voter = voter;
        }
    }
}
