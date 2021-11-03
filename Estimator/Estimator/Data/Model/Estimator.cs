namespace Estimator.Data.Model
{
    public class Estimator
    {
        public string Name { get; set; }
        public string Estimation { get; set; }

        public Estimator(string name, string estimation)
        {
            this.Name = name;
            this.Estimation = estimation;
        }

        public Estimator(string name)
        {
            this.Name = name;
            this.Estimation = string.Empty;
        }
    }
}