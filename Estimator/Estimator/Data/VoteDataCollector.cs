namespace Estimator.Data
{
    public class VoteDataCollector
    {
        public TShirtVoteResult tShirtVote = new TShirtVoteResult();

        public async void TShirtVoteCollector(string voteResult)
        {
            this.tShirtVote.TShirtVoteResultbyVoter(voteResult);
        }

        public void TShirtVoteStart()
        {
            this.tShirtVote.AdminStartTShirtVoting();
        }
        public TShirtVoteResult TShirtVoteClose()
        {
            return this.tShirtVote;
        }
    }
}
