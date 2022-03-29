namespace Exam.Doodle
{
    public class Doodle
    {
        public Doodle(string id, string title, int visits, bool isAd, double revenue)
        {
            this.Id = id;
            this.Title = title;
            this.Visits = visits;
            this.IsAd = isAd;
            this.Revenue = revenue;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public int Visits { get; set; }

        public bool IsAd { get; set; }

        public double Revenue { get; set; }
    }
}
