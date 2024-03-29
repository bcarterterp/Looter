using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureStack{

    public const int SPECIALTY_CARDS = 2;

    List<int> cards;

    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    public int Pop()
    {
        int temp = cards[cards.Count-1];
        cards.RemoveAt(cards.Count - 1);
        return temp;
    }

    public void PopAll()
    {
        for(int i = cards.Count; i > 0; i--)
        {
            Pop();
        }
    }

    public void Push(int card)
    {
        cards.Add(card);
    }

    public int CardCount()
    {
        if (cards == null)
        {
            return 0;
        }
        else
        {
            return cards.Count;
        }
    }

    public void CreateDungeonDeck()
    {
        cards = new List<int>();
		Push((int)CardType.ITEM);
        return;
    }
}
