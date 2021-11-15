namespace Estimator.Data.Model
{
    public class DiagramValues
    {
        public string EstimationCategory { get; set; }
        public string EstimationCount { get; set; }

        public DiagramValues(string estimateCategory, string estimateCount)
        {
            this.EstimationCategory = estimateCategory;
            this.EstimationCount = estimateCount;
        }
    }
}