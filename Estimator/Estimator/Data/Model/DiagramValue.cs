namespace Estimator.Data.Model
{
    public class DiagramValue
    {
        public string EstimationCategory { get; set; }
        public string EstimationCount { get; set; }

        public DiagramValue(string estimateCategory, string estimateCount)
        {
            this.EstimationCategory = estimateCategory;
            this.EstimationCount = estimateCount;
        }
    }
}