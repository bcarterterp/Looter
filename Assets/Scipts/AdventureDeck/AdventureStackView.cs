using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureStackView : MonoBehaviour
{
    AdventureStack deck;
    List<GameObject> livePile;
    int cardCount = 0;
    Vector3 start;
    
    public GameObject cardPrefab;
    public bool faceUp = false;
    public bool showReverseCardOrder = false;
    public float cardOffset;

    private void Start()
    {
        start = GetComponent<Transform>().position;
        //deck = GetComponent<AdventureStack>();
        //livePile = new List<GameObject>();
        //ShowCards();
    }

    private void Deck_DeckAdjusted(object sender, DeckAdjustedEventArgs args)
    {
        if (args.CardRemoved)
        {
            Destroy(livePile[livePile.Count-1]);
            livePile.RemoveAt(livePile.Count-1);
        }
        else
        {
            AddCard(args.CardIndex);
        }
    }

    private void ShowCards()
    {
        foreach (int i in deck.GetCards())
        {
            AddCard(i);
        }
    }

    void AddCard(int cardType)
    {
        float co = cardOffset * cardCount;
        Vector3 position = start + new Vector3(co, 0f);
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.localScale = GetComponent<Transform>().localScale;
        cardCopy.transform.position = position;


        LootCardModel cardModel = cardCopy.GetComponent<LootCardModel>();
        cardModel.cardType = cardType;
        cardModel.ToggleFace(faceUp);
        livePile.Add(cardCopy);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (showReverseCardOrder)
        {
            spriteRenderer.sortingOrder = deck.CardCount() - 1 - cardCount;
        }
        else
        {
            spriteRenderer.sortingOrder = cardCount;
        }
        cardCount++;
    }
}
