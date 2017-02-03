using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour {

    CardStack deck;
    Dictionary<int, GameObject> fetchedCards;
    int lastCount;

    public Vector3 start;
    public GameObject cardPrefab;
    public bool faceUp = false;
    public bool reversePlayerOrder = false;
    public float cardOffset;

    private void Start()
    {
        fetchedCards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardStack>();
        ShowCards();
        lastCount = deck.CardCount();
        deck.CardRemoved += Deck_CardRemoved;
    }

    private void Deck_CardRemoved(object sender, CardRemovedEventArgs args)
    {
        if (fetchedCards.ContainsKey(args.CardIndex))
        {
            Destroy(fetchedCards[args.CardIndex]);
            fetchedCards.Remove(args.CardIndex);
        }
    }

    private void Update()
    {
        if(lastCount != deck.CardCount())
        {
            ShowCards();
            lastCount = deck.CardCount();
        }
    }

    private void ShowCards()
    {
        int cardCount = 0;

        foreach (int i in deck.GetCards())
        {
            float co = cardOffset * cardCount;
            Vector3 temp = start + new Vector3(co, 0f);
            AddCard(temp, i, cardCount);
            cardCount++;
        }
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex)
    {
        if (!fetchedCards.ContainsKey(cardIndex))
        {

            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            cardCopy.transform.position = position;

            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.cardIndex = cardIndex;
            cardModel.ToggleFace(faceUp);
            fetchedCards.Add(cardIndex, cardCopy);

            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            if (reversePlayerOrder)
            {
                spriteRenderer.sortingOrder = 3 - positionalIndex;
            }
            else
            {
                spriteRenderer.sortingOrder = positionalIndex;
            }
        }
    }
    
}
