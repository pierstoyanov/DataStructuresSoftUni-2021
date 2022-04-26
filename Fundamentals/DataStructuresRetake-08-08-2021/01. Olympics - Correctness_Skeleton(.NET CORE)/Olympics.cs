using System;
using System.Collections.Generic;

public class Olympics : IOlympics
{
    private SortedDictionary<int, Competitor> competitors = new SortedDictionary<int, Competitor>();

    private Dictionary<int, Competition> games = new Dictionary<int, Competition>();

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        CheckExistsInCollection(id, games);
        // limit?
        games.Add(id, new Competition(name, id, participantsLimit));
    }

    public void AddCompetitor(int id, string name)
    {
        CheckExistsInCollection(id, competitors);

        competitors.Add(id, new Competitor(id, name));
    }

    public void Compete(int competitorId, int competitionId)
    {
        CheckExistsInCollection(competitorId, competitors);
        CheckExistsInCollection(competitionId, games);

        games[competitionId].Competitors.Add(competitors[competitorId]);
        //games[competitionId].Score += competitors[competitorId].TotalScore;
    }

    public int CompetitionsCount()
    {
        return games.Count;
    }

    public int CompetitorsCount()
    {
        return competitors.Count;
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (games.ContainsKey(competitionId))
        {
            return games[competitionId].Competitors.Contains(comp);
        }

        return false;
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        CheckExistsInCollection(competitorId, competitors);
        CheckExistsInCollection(competitionId, games);

        games[competitionId].Competitors.Remove(competitors[competitorId]);
        competitors[competitorId].TotalScore -= games[competitionId].Score;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Competition GetCompetition(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        throw new NotImplementedException();
    }

    private void CheckExistsInCollection(int key, IDictionary<int, Competitor> collection)
    {
        if (!collection.ContainsKey(key))
        {
            throw new ArgumentException();
        }
    }

    private void CheckExistsInCollection(int key, IDictionary<int, Competition> collection)
    {
        if (!collection.ContainsKey(key))
        {
            throw new ArgumentException();
        }
    }
}