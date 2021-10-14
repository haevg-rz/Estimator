using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Data.Model
{
    public class DiagramData
    {
        public string X { get; set; }
        public string Y { get; set; }

        public DiagramData(string voteTopic, string voteNumber)
        {
            this.X = voteTopic;
            this.Y = voteNumber;
        }

    }
}
