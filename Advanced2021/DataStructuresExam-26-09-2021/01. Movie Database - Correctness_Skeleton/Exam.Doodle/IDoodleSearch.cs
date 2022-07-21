using System.Collections.Generic;

namespace Exam.Doodle
{
    public interface IDoodleSearch
    {
        void AddDoodle(Doodle doodle);

        void RemoveDoodle(string doodleId);

        int Count { get; }

        bool Contains(Doodle doodle);

        Doodle GetDoodle(string id);

        double GetTotalRevenueFromDoodleAds();

        void VisitDoodle(string title);

        IEnumerable<Doodle> SearchDoodles(string searchQuery);

        IEnumerable<Doodle> GetDoodleAds();

        IEnumerable<Doodle> GetTop3DoodlesByRevenueThenByVisits();
    }
}
