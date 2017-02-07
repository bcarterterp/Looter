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
		Push((int)CardType.MONSTER);
        return;
        int deckSize = Random.Range(0, 5)+5;
        for (int i = 0; i < deckSize; i++)
        {
            int cardProbability = Random.Range(0,100);
            if (cardProbability < 80)
            {
                Push((int)CardType.MONSTER);
            }
            else
            {
                switch (Random.Range(0, SPECIALTY_CARDS))
                {
                    case 0:
                        Push((int)CardType.ITEM);
                        break;
                    case 1:
                        Push((int)CardType.POTION);
                        break;
                }
            }
        }
    }
}
