using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Doodle
{
    public class DoodleSearch : IDoodleSearch
    {
        Dictionary<string, Doodle> titleDoodle = new Dictionary<string, Doodle>();
        Dictionary<string, Doodle> idDoodle = new Dictionary<string, Doodle>();

        public int Count { get; private set; }

        public void AddDoodle(Doodle doodle)
        {
            if (!titleDoodle.ContainsKey(doodle.Title))
            {
                titleDoodle.Add(doodle.Title, doodle);
                idDoodle.Add(doodle.Id, doodle);
                this.Count++;
            }
        }

        public bool Contains(Doodle doodle)
        {
            return this.ContainsByTitle(doodle.Title);
        }
        public bool ContainsByTitle(string title)
        {
            if (titleDoodle.ContainsKey(title))
            {
                return true;
            }
            return false;
        }

        public Doodle ReturnByTitle(string title)
        {
            if (!titleDoodle.ContainsKey(title))
            {
                throw new ArgumentException();
            }
            return titleDoodle[title];
        }

        public bool ContainsById(string id)
        {
            if (idDoodle.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public Doodle GetDoodle(string id)
        {
            if (!idDoodle.ContainsKey(id))
            {
                throw new ArgumentException();
            }
            return idDoodle[id];
        }

        public IEnumerable<Doodle> GetDoodleAds()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Doodle> GetTop3DoodlesByRevenueThenByVisits()
        {
            throw new System.NotImplementedException();
        }

        public double GetTotalRevenueFromDoodleAds()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDoodle(string doodleId)
        {
            if (!this.ContainsById(doodleId))
            {
                throw new ArgumentException();
            }

            var doodleToRemove = idDoodle[doodleId];
            titleDoodle.Remove(doodleToRemove.Title);
            idDoodle.Remove(doodleId);
            this.Count--;
        }

        public IEnumerable<Doodle> SearchDoodles(string searchQuery)
        {
            return SearchDoodlesSlow(searchQuery);
        }

        public IEnumerable<Doodle> SearchDoodlesSlow(string searchQuery)
        {
            List<Doodle> result;

            var SearchedDoodles = titleDoodle.Where(kv => kv.Key.Contains(searchQuery)).Select(kvp => kvp.Value).ToList();

            result = SearchedDoodles
                .OrderByDescending(x => x.IsAd)
                .ThenByDescending(y => y.Visits)
                .ToList();

            return result;
        }

        public void VisitDoodle(string title)
        {
            var visitedDoodle = ReturnByTitle(title);
            visitedDoodle.Visits++;
        }
    }
}
