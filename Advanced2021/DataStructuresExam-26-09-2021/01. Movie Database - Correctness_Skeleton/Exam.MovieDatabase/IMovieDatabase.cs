using System.Collections.Generic;

namespace Exam.MovieDatabase
{
    public interface IMovieDatabase
    {
        void AddMovie(Movie movie);

        void RemoveMovie(string movieId);

        int Count { get; }

        bool Contains(Movie movie);

        IEnumerable<Movie> GetMoviesByActor(string actorName);

        IEnumerable<Movie> GetMoviesByActors(List<string> actors);

        IEnumerable<Movie> GetMoviesByYear(int releaseYear);

        IEnumerable<Movie> GetMoviesInRatingRange(double lowerBound, double upperBound);

        IEnumerable<Movie> GetAllMoviesOrderedByActorPopularityThenByRatingThenByYear();
    }
}
