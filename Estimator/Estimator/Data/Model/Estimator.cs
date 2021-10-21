namespace Estimator.Data
{
    public class Estimator
    {
        public string Name { get; set; }
        public string Vote { get; set; }

        public Estimator(string name, string vote)
        {
            this.Name = name;
            this.Vote = vote;
        }

        public Estimator(string name)
        {
            this.Name = name;
            this.Vote = string.Empty;
        }
    }
}