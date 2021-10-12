using System;
using System.Collections.Generic;

namespace Estimator.Data.Model
{
    public class VotingResults
    {
        public string VotingVariety { get; set; }
        public int VotingQuantity { get; set; }
        public List<string> Voter { get; set; }

        public VotingResults()
        {
            this.VotingVariety = String.Empty;
            this.VotingQuantity = 0;
            this.Voter = new List<string>();
        }
    }
}
