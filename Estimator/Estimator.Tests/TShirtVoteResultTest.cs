using Estimator.Data;
using Xunit;

namespace Estimator.Test
{
    public class TShirtVoteResultTest
    {
        private string sizeS = "sizeS";

        [Fact]
        public void AdminStartTShirtVotingTest()
        {
            #region Assign

            var adminStartVoting = new TShirtVoteResult();

            #endregion

            #region Act

            adminStartVoting.AdminStartTShirtVoting();

            #endregion

            #region Assert

            Assert.Equal(0, adminStartVoting.SizeS);
            Assert.Equal(0, adminStartVoting.SizeM);
            Assert.Equal(0, adminStartVoting.SizeL);
            Assert.Equal(0, adminStartVoting.SizeXL);
            Assert.Equal(0, adminStartVoting.SizeXXL);

            #endregion
        }

        [Fact]
        public void TShirtVoteResultbyVoterTest()
        {
            #region Assign

            var voteResultByVoter = new TShirtVoteResult();

            #endregion

            #region Act

            voteResultByVoter.TShirtVoteResultbyVoter(this.sizeS);

            #endregion

            #region Assert

            Assert.Equal(1, voteResultByVoter.SizeS);
            Assert.Equal(0, voteResultByVoter.SizeM);
            Assert.Equal(0, voteResultByVoter.SizeL);
            Assert.Equal(0, voteResultByVoter.SizeXL);
            Assert.Equal(0, voteResultByVoter.SizeXXL);

            #endregion
        }
    }
}