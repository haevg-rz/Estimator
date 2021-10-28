using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Data.Model
{
    public class DiagramData
    {
        public string EstimationTopic { get; set; }
        public string EstimationCount { get; set; }

        public DiagramData(string estimateTopic, string estimateCount)
        {
            this.EstimationTopic = estimateTopic;
            this.EstimationCount = estimateCount;
        }

    }
}
