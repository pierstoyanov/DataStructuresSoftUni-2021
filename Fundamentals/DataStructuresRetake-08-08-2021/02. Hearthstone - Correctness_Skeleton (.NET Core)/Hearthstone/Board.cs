using System;
using System.Collections.Generic;
using System.Text;

public class Board : IBoard
{ 
    List<Card> cards = new List<Card>();

    public bool Contains(string name)
    {
        foreach (var item in cards)
        {
            if (item.Name == name)
                return true;
        }
        return false;
    }

    public int Count()
    {
        return cards.Count;
    }

    public void Draw(Card card)
    {
        if (this.Contains(card.Name))
            throw new ArgumentException();

        cards.Add(card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        throw new NotImplementedException();
    }

    public void Heal(int health)
    {
        var smallestHealth = cards[0];

        foreach (var item in cards)
        {
            if (item.Health < smallestHealth.Health)
                smallestHealth = item;
        }

        smallestHealth.Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        throw new NotImplementedException();
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        var attackerCard = cards.Find(x => x.Name == attackerCardName);
        var defendCard = cards.Find(x => x.Name == attackedCardName);

        if (attackerCard == null || defendCard == null)
            throw new ArgumentException();

        if (!attackerCard.Level.Equals(defendCard.Level))
            throw new ArgumentException();

        if (attackerCard.Health <= 0)
            throw new ArgumentException();
        if (defendCard.Health <= 0)
            throw new ArgumentException();


        defendCard.Health -= attackerCard.Damage;

        if (attackerCard.Damage - defendCard.Health <= 0)
        {
            defendCard.IsDefeated = true;
            attackerCard.Score += defendCard.Level;
        }

    }

    public void Remove(string name)
    {
        var card = this.cards.Find(x => x.Name == name);
        if (card == null)
            throw new ArgumentException();

        cards.Remove(card);
    }

    public void RemoveDeath()
    {
        foreach (var item in cards.ToArray())
        {
            if (item.Health <= 0)
                cards.Remove(item);
        }
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        var result = new List<Card>();

        foreach (var item in cards)
        {
            if (item.Level == level)
                result.Add(item);
        }

        result.Sort((a, b) => b.Level.CompareTo(a.Level));

        return result;
    }
}