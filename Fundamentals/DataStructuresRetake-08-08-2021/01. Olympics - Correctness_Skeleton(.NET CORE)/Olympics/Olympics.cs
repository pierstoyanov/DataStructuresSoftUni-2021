using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{

    private MaxHeap<Competitor> competitorHeap = new MaxHeap<Competitor>();
    //private List<Competitor> competitorList = new List<Competitor>();
    private Dictionary<int, string> competitorsDict = new Dictionary<int, string>();
    private Dictionary<int, int> competitorsDictScore = new Dictionary<int, int>();

    private MaxHeap<Competition> CompetitionHeap = new MaxHeap<Competition>();
    //private List<Competition> CompetitionList = new List<Competition>();
    private Dictionary<int, string> CompetitionDict = new Dictionary<int, string>();


    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (CompetitionDict.ContainsKey(id))
            throw new ArgumentException();

        Competition newCompetition = new Competition(name, id, participantsLimit);
        newCompetition.Competitors = new List<Competitor>();
        //CompetitionList.Add(newCompetition);
        CompetitionHeap.Add(newCompetition);

        CompetitionDict.Add(id, name);
    }

    public void AddCompetitor(int id, string name)
    {
        if (competitorsDict.ContainsKey(id))
            throw new ArgumentException();

        Competitor newCompetitor = new Competitor(id, name);
        //competitorList.Add(newCompetitor);
        competitorHeap.Add(newCompetitor);
        competitorsDict.Add(id, name);
        competitorsDictScore.Add(id, (int)newCompetitor.TotalScore);
    }

    public void Compete(int competitorId, int competitionId)
    {
        var competitor = competitorHeap.Heap.Find(x => x.Id == competitorId);
        var competition = CompetitionHeap.Heap.Find(x => x.Id == competitionId);

        if (competitor == null || competition == null)
            throw new ArgumentException();

        competitor.TotalScore += competition.Score;
        competitorsDictScore[competitorId] += competition.Score;
        competition.Competitors.Add(competitor);
    }

    public int CompetitionsCount()
    {
        return CompetitionHeap.Size;
    }

    public int CompetitorsCount()
    {
        return competitorHeap.Size;
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        var competition = CompetitionHeap.Heap.Find(x => x.Id == competitionId);

        if (competition == null)
            throw new ArgumentException();

        foreach (var item in competition.Competitors)
        {
            if (item.Id.Equals(comp.Id))
                return true;
        }
        return false;
    }

    public void Disqualify(int competitionId, int competitorId)
    {


        if (!CompetitionDict.ContainsKey(competitionId))
            throw new ArgumentException();

        var competition = CompetitionHeap.Heap.Find(x => x.Id == competitionId);

        var player = competition.Competitors.FirstOrDefault(x => x.Id == competitorId);
        competition.Competitors.Remove(player);
        player.TotalScore -= competition.Score;
        competitorsDictScore[player.Id] -= competition.Score;

    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        var temp = competitorsDictScore.OrderBy(kvp => kvp.Value);
        var tempMin = temp.First();
        var tempMax = temp.Last();
        var result = new List<Competitor>();

        if (tempMin.Value > min && tempMax.Value < max)
           return result;

        foreach (var item in competitorsDictScore)
        {
            if (item.Value > min && item.Value <= max)
            {
                foreach (var j in competitorHeap.Heap)
                {
                    if (j.Id == item.Key)
                        result.Add(j);
                }
            }
        }

        result.Sort((a, b) => a.Id.CompareTo(b.Id));
        return result;
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        if (!competitorsDict.ContainsValue(name))
            throw new ArgumentException();

        var result = new List<Competitor>();
        foreach (var item in competitorHeap)
        {
            if (item.Name == name)
                result.Add(item);
        }

        result.Sort((a, b) => a.Id.CompareTo(b.Id));

        return result;
    }

    public Competition GetCompetition(int id)
    {
        if (!CompetitionDict.ContainsKey(id))
            throw new ArgumentException();

        var competition = CompetitionHeap.Heap.Find(x => x.Id == id);
 
        return competition;
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        var temp = competitorsDict.OrderBy(kvp => kvp.Value.Length);
        var tempMin = temp.First();
        var tempMax = temp.Last();

        var result = new List<Competitor>();

        if (tempMin.Value.Length > min && tempMax.Value.Length < max)
            return result;

        foreach (var item in competitorsDict)
        {
            if (item.Value.Length >= min && item.Value.Length <= max)
            {
                foreach (var j in competitorHeap.Heap)
                {
                    if (j.Id == item.Key)
                        result.Add(j);
                }
            }
        }

        result.Sort((a, b) => a.Id.CompareTo(b.Id));
        return result;
    }
}

