using Xunit;

namespace Estimator.Test
{
    public class VoteDataCollectorTest
    {
        private Data.VoteDataCollector dataCollector { get; } = new Data.VoteDataCollector();
        private string sizeS = "sizeS";
        private string sizeM = "sizeM";
        private string sizeL = "sizeL";
        private string sizeXL = "sizeXL";
        private string sizeXXL = "sizeXXL";

        [Fact]
        public void TShirtVoteTest()
        {
            #region Assign

            #endregion

            #region Act

            this.dataCollector.TShirtVoteStart();
            this.dataCollector.TShirtVoteCollector(this.sizeS);
            this.dataCollector.TShirtVoteCollector(this.sizeS);
            this.dataCollector.TShirtVoteCollector(this.sizeM);
            this.dataCollector.TShirtVoteCollector(this.sizeL);
            this.dataCollector.TShirtVoteCollector(this.sizeXL);
            this.dataCollector.TShirtVoteCollector(this.sizeXL);
            this.dataCollector.TShirtVoteCollector(this.sizeXXL);
            var result = this.dataCollector.TShirtVoteClose();

            #endregion

            #region Assert

            Assert.Equal(2, result.SizeS);
            Assert.Equal(1, result.SizeM);
            Assert.Equal(1, result.SizeL);
            Assert.Equal(2, result.SizeXL);
            Assert.Equal(1, result.SizeXXL);

            #endregion
        }
    }
}