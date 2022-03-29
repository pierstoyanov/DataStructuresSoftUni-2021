using System;

public class Competitor : IComparable<Competitor>
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public int CompareTo(Competitor other)
    {
        if (this.TotalScore < other.TotalScore)
        {
            return 1;
        }
        else if (this.TotalScore > other.TotalScore)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}