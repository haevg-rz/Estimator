namespace Estimator.Data.Model
{
    public class DiagramData
    {
        public string EstimationCategory { get; set; }
        public string EstimationCount { get; set; }

        public DiagramData(string estimateCategory, string estimateCount)
        {
            this.EstimationCategory = estimateCategory;
            this.EstimationCount = estimateCount;
        }

    }
}
