using System;
using System.Collections.Generic;

public class Competition : IComparable<Competition>
{
    public Competition(string name, int id, int score)
    {
        this.Name = name;
        this.Id = id;
        this.Score = score;

    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int Score { get; set; }

    public ICollection<Competitor> Competitors { get; set; }

    public int CompareTo(Competition other)
    {
        if (this.Score < other.Score)
        {
            return 1;
        }
        else if (this.Score > other.Score)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
