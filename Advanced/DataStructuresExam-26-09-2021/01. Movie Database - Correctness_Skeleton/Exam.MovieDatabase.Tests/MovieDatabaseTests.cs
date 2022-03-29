using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MovieDatabase.Tests
{
    public class MovieDatabaseTests
    {
        private IMovieDatabase movieDatabase;

        [SetUp]
        public void Setup()
        {
            this.movieDatabase = new MovieDatabase();
        }

        private Movie GetRandomMovie()
        {
            return new Movie(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    (int)Math.Min(1, new Random().Next(0, 2_000)),
                    (int)Math.Min(1, new Random().Next(0, 10)),
                    new List<string>(Enumerable.Range(1, (int)Math.Min(1, new Random().Next(0, 10))).Select(x => Guid.NewGuid().ToString()).ToList()));
        }

        // Correctness Tests

        [Test]
        public void TestAddMovie_WithCorrectData_ShouldSuccessfullyAddMovie()
        {
            this.movieDatabase.AddMovie(this.GetRandomMovie());
            this.movieDatabase.AddMovie(this.GetRandomMovie());

            Assert.AreEqual(2, this.movieDatabase.Count);
        }

        [Test]
        public void TestContains_WithExistentMovie_ShouldReturnTrue()
        {
            Movie randomMovie = this.GetRandomMovie();

            this.movieDatabase.AddMovie(randomMovie);

            Assert.IsTrue(this.movieDatabase.Contains(randomMovie));
        }

        [Test]
        public void TestContains_WithNonexistentMovie_ShouldReturnFalse()
        {
            Movie randomMovie = this.GetRandomMovie();

            this.movieDatabase.AddMovie(randomMovie);

            Assert.IsFalse(this.movieDatabase.Contains(this.GetRandomMovie()));
        }

        [Test]
        public void TestCount_With5Movies_ShouldReturn5()
        {
            this.movieDatabase.AddMovie(this.GetRandomMovie());
            this.movieDatabase.AddMovie(this.GetRandomMovie());
            this.movieDatabase.AddMovie(this.GetRandomMovie());
            this.movieDatabase.AddMovie(this.GetRandomMovie());
            this.movieDatabase.AddMovie(this.GetRandomMovie());

            Assert.AreEqual(5, this.movieDatabase.Count);
        }

        [Test]
        public void TestGetByActors_WithCorrectActors_ShouldReturnCorrectlyOrderedMovies()
        {
            Movie Movie = new Movie("asd", "bsd", 3000, 3000, new List<string>(new string[] { "Pesho", "Gosho" }));
            Movie Movie2 = new Movie("dsd", "esd", 2020, 5000, new List<string>(new string[] { "Pesho", "Gosho" }));
            Movie Movie3 = new Movie("hsd", "isd", 2019, 5000, new List<string>(new string[] { "Pesho", "Gosho" }));
            Movie Movie4 = new Movie("jsd", "lsd", 3000, 3000, new List<string>(new string[] { "Pesho", "Gosho" }));

            this.movieDatabase.AddMovie(Movie);
            this.movieDatabase.AddMovie(Movie2);
            this.movieDatabase.AddMovie(Movie3);
            this.movieDatabase.AddMovie(Movie4);

            List<Movie> movies = new List<Movie>(this.movieDatabase.GetMoviesByActors(new List<string>(new string[] { "Pesho", "Gosho" })));

            Assert.AreEqual(4, movies.Count);
            Assert.AreEqual(Movie2, movies[0]);
            Assert.AreEqual(Movie3, movies[1]);
            Assert.AreEqual(Movie, movies[2]);
            Assert.AreEqual(Movie4, movies[3]);
        }
    }
}