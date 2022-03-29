using System.Collections.Generic;

namespace Exam.MovieDatabase
{
    public class Movie
    {
        public Movie(string id, string name, int releaseYear, double rating, List<string> actors)
        {
            this.Id = id;
            this.Name = name;
            this.ReleaseYear = releaseYear;
            this.Rating = rating;
            this.Actors = actors;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public double Rating { get; set; }

        public List<string> Actors { get; set; }
    }
}
