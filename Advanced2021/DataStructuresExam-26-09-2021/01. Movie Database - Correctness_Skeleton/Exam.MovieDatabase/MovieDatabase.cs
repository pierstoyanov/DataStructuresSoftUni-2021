using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MovieDatabase
{
    public class MovieDatabase : IMovieDatabase
    {
        Dictionary<string, Movie> idMovie = new Dictionary<string, Movie>();
        SortedDictionary<double, Movie> ratingMovie = new SortedDictionary<double, Movie>();
        //AVLTree<Movie> ratingAVL = new AVLTree<Movie>();

        public int Count { get; private set; }

        public void AddMovie(Movie movie)
        {
            if (!idMovie.ContainsKey(movie.Id))
            {
                idMovie.Add(movie.Id, movie);
                //ratingMovie.Add(movie.Rating, movie);
                //ratingAVL.Insert(movie);
                this.Count++;
            }
        }

        public bool Contains(Movie movie)
        {
            return this.ContainsById(movie.Id);
        }

        public bool ContainsById(string id)
        {
            if (idMovie.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Movie> GetAllMoviesOrderedByActorPopularityThenByRatingThenByYear()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMoviesByActor(string actorName)
        {
            return GetMoviesByActorSlow(actorName);
        }

        public IEnumerable<Movie> GetMoviesByActorSlow(string actorName)
        {
            List<Movie> result;

            var moviesByActor = idMovie.Where(kv => kv.Value.Actors.Contains(actorName)).Select(kvp => kvp.Value).ToList();

            result = moviesByActor.OrderByDescending(x => x.Rating).ThenByDescending(y => y.ReleaseYear).ToList();

            return result;
        }

        public IEnumerable<Movie> GetMoviesByActors(List<string> actors)
        {
            List<Movie> result = new List<Movie>();

            //var moviesByActor = ratingMovie.Where(kv => kv.Value.Actors.Any(x => actors.Any(y=> y.Equals(x)))).Select(kvp => kvp.Value).ToList();

            return GetMoviesByActorsSlow(actors);
        }
        public IEnumerable<Movie> GetMoviesByActorsSlow(List<string> actors)
        {
            List<Movie> result;

            var moviesByActors = idMovie.Where(kv => !kv.Value.Actors.Except(actors).Any()).Select(kvp => kvp.Value);

            result = moviesByActors.OrderByDescending(x => x.Rating).ThenByDescending(y => y.ReleaseYear).ToList();

            return result;
        }

        public IEnumerable<Movie> GetMoviesByYear(int releaseYear)
        {
            return GetMoviesByYearSlow(releaseYear);
        }

        public IEnumerable<Movie> GetMoviesByYearSlow(int releaseYear)
        {
            List<Movie> result;

            var moviesByYear = idMovie.Where(kv => kv.Value.ReleaseYear == releaseYear).Select(kvp => kvp.Value).ToList();

            result = moviesByYear.OrderByDescending(x => x.Rating).ToList();

            return result;
        }

        public IEnumerable<Movie> GetMoviesInRatingRange(double lowerBound, double upperBound)
        {
            return GetMoviesInRatingRangeSlow(lowerBound, upperBound);
        }

        public IEnumerable<Movie> GetMoviesInRatingRangeSlow(double lowerBound, double upperBound)
        {
            List<Movie> result;

            var moviesByRange = idMovie.Where(kv => lowerBound <= kv.Value.Rating && kv.Value.Rating >= upperBound).Select(kvp => kvp.Value).ToList();

            result = moviesByRange.OrderByDescending(x => x.Rating).ToList();

            return result;
        }

        public void RemoveMovie(string movieId)
        {
            if (!this.ContainsById(movieId))
            {
                throw new ArgumentException();
            }
            var movieToRemove = idMovie[movieId];
            ratingMovie.Remove(movieToRemove.Rating);
            idMovie.Remove(movieId);
            this.Count--;
        }
    }
}
