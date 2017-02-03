using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibray : MonoBehaviour {

    private List<int> cardLibrary;

	// Use this for initialization
	void Start () {
        cardLibrary = new List<int>();
        for(int i = 0; i < (int)CardType.TOTAL_CARD_TYPES; i++)
        {
            cardLibrary.Add(0);
        }
	}

    public void IncreseCardLevel(int cardType)
    {
        if(cardType < cardLibrary.Count)
        {
            cardLibrary[cardType]++;
        }
    }

    public void SetCardLevel(int cardType, int level)
    {
        if (cardType < cardLibrary.Count)
        {
            cardLibrary[cardType] = level;
        }
    }
	
	public int GetCardLevel(int cardType)
    {
        int level = 0;
        if (cardType < cardLibrary.Count)
        {
            level = cardLibrary[cardType];
        }
        return level;
    }

    public int LibraryCount()
    {
        return cardLibrary.Count;
    }
}