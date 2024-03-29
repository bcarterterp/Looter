﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {

    List<int> cards;

    public bool isGameDeck;

    public IEnumerable<int> GetCards()
    {
        foreach(int i in cards)
        {
            yield return i;
        }
    }

    public event CardRemovedEventHandler CardRemoved;

    public int Pop()
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        if (CardRemoved != null)
        {
            CardRemoved(this, new CardRemovedEventArgs(temp));
        }
        return temp;
    }

    public void Push(int card)
    {
        cards.Add(card);
    }

    public int CardCount()
    {
        if(cards == null)
        {
            return 0;
        }
        else
        {
            return cards.Count;
        }
    }

    public void CreateDeck()
    {
        for(int i = 0; i < 4; i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        while(n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

    private void Awake()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            CreateDeck();
        }
    }

}
