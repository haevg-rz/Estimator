using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Data.Model
{
    public class VotingResults
    {
        public string VotingVariety { get; set; }
        public int VotingQuantity { get; set; }

        public VotingResults()
        {
            this.VotingVariety = String.Empty;
            this.VotingQuantity = 0;
        }
    }
}
