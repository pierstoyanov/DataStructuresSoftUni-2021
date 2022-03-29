using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Exam.Doodle.Tests
{
    public class DoodleSearchTests
    {
        private IDoodleSearch doodleSearch;

        [SetUp]
        public void Setup()
        {
            this.doodleSearch = new DoodleSearch();
        }

        private Doodle GetRandomDoodle()
        {
            return new Doodle(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    (int)Math.Min(1, new Random().Next(0, 2_000)),
                    ((int)Math.Min(1, new Random().Next(0, 2_000)) % 2 == 1),
                    (int)Math.Min(1, new Random().Next(0, 1_000)));
        }

        // Correctness Tests
        [Test]
        public void TestAddDoodle_WithCorrectData_ShouldSuccessfullyAddDoodle()
        {
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());

            Assert.AreEqual(2, this.doodleSearch.Count);
        }

        [Test]
        public void TestContains_WithExistentDoodle_ShouldReturnTrue()
        {
            Doodle randomDoodle = this.GetRandomDoodle();

            this.doodleSearch.AddDoodle(randomDoodle);

            Assert.IsTrue(this.doodleSearch.Contains(randomDoodle));
        }

        [Test]
        public void TestContains_WithNonexistentDoodle_ShouldReturnFalse()
        {
            Doodle randomDoodle = this.GetRandomDoodle();

            this.doodleSearch.AddDoodle(randomDoodle);

            Assert.IsFalse(this.doodleSearch.Contains(this.GetRandomDoodle()));
        }

        [Test]
        public void TestCount_With5Doodles_ShouldReturn5()
        {
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());
            this.doodleSearch.AddDoodle(this.GetRandomDoodle());

            Assert.AreEqual(5, this.doodleSearch.Count);
        }

        [Test]
        public void TestSearchDoodles_WithCorrectDoodles_ShouldReturnCorrectlyOrderedData()
        {
            Doodle Doodle = new Doodle("asd", "bbbsd", 4000, true, 5.5);
            Doodle Doodle2 = new Doodle("nsd", "eesd", 4000, false, 5.6);
            Doodle Doodle3 = new Doodle("dsd", "ddsd", 5000, false, 5.7);
            Doodle Doodle4 = new Doodle("hsd", "zsd", 4000, true, 4.8);
            Doodle Doodle5 = new Doodle("qsd", "qsd", 4001, true, 4.8);
            Doodle Doodle6 = new Doodle("zsd", "ds", 5000, false, 5.7);

            this.doodleSearch.AddDoodle(Doodle);
            this.doodleSearch.AddDoodle(Doodle2);
            this.doodleSearch.AddDoodle(Doodle3);
            this.doodleSearch.AddDoodle(Doodle4);
            this.doodleSearch.AddDoodle(Doodle5);
            this.doodleSearch.AddDoodle(Doodle6);

            List<Doodle> Doodles = new List<Doodle>(this.doodleSearch.SearchDoodles("sd"));

            Assert.AreEqual(5, Doodles.Count);
            Assert.AreEqual(Doodle5, Doodles[0]);
            Assert.AreEqual(Doodle4, Doodles[1]);
            Assert.AreEqual(Doodle, Doodles[2]);
            Assert.AreEqual(Doodle3, Doodles[3]);
            Assert.AreEqual(Doodle2, Doodles[4]);
        }
    }
}