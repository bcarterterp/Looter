using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour, CardSelectedListener
{
	private const float CARD_WIDTH = 160f;
	private const float CARD_HEIGHT = 100f;
    private const float CARD_SPACE = 40f;

    private AdventureLogic logic;
    private SelectableCard activeCard;
    private SelectableCard[] activeCards;
    private bool[] optionsSelected;
    private bool multiSelect;

    public Text health, atk, def, storyText;
	public GameObject prefab;
    public Canvas canvas;

    private void Start()
    {
        logic = new AdventureLogic();
        logic.StartGame();
        UpdatePanel();
		activeCard = Instantiate(prefab).GetComponent<SelectableCard>();
        activeCard.transform.position = new Vector3(0, 100f);
        activeCard.transform.SetParent(canvas.transform, false);
        activeCard.ShowCard(-1, false);
        multiSelect = false;
    }

    public void Next()
    {

        if (logic.GetActiveCard() == (int)CardType.POI)
        {
            logic.Restart();
        }

        if (logic.GetActiveCard() == -1)
        {
            int card = logic.DrawCard();
            activeCard.ShowCard(card, false);
        }
        else
        {
            logic.InteractWithActiveCard();
            switch (logic.GetActiveCard())
            {
                case (int)CardType.MERCHANT:
                    MerchantFlow();
                    break;
                case -1:
                    activeCard.ShowCard(-1, false);
                    break;
            }
            UpdatePanel();
        }

        if (logic.GetHero().getCurrHealth() == 0)
        {
            logic.WipeAllData();
        }
    }

    public void MerchantFlow()
    {
		switch (logic.GetLogicStage ()) {
			case 1:
				ShowMerchantItems();
				break;
			case 2:
				OptionsSelected ();
				break;
		}
    }

    public void ShowMerchantItems()
    {
        activeCard.HideCard();
        Item[] items = logic.GetItems();
        activeCards = new SelectableCard[items.Length];
		optionsSelected = new bool[items.Length];
        float startingOffset = 0;
        switch (items.Length)
        {
            case 2:
				startingOffset = .5f * (CARD_WIDTH + CARD_SPACE);
                break;
			case 3:
				startingOffset = CARD_WIDTH + CARD_SPACE;
                break;
        }
		for (int i = 0; i < items.Length; i++) {
			optionsSelected [i] = false;
			float offset = -startingOffset + i * (CARD_WIDTH + CARD_SPACE);
			activeCards [i] = Instantiate(prefab).GetComponent<SelectableCard>();
			activeCards [i].transform.position = new Vector3 (offset, CARD_HEIGHT);
			activeCards [i].transform.SetParent (canvas.transform, false);
            activeCards[i].ShowItemCard(items[i], true);
			int option = i;
		}
    }

	public void OptionsSelected(){
		for (int i = 0; i < activeCards.Length; i++) {
			if (activeCards[i].IsSelected()) {
				logic.InteractWithActiveCard (i);
                break;
			}
		}
	}

    public void Decline()
    {
        logic.Decline();
        activeCard.ShowCard(-1, false);
    }

    public void ClearGame()
    {
        logic.WipeAllData();
    }

    private void UpdatePanel()
    {
        Hero hero = logic.GetHero();
        health.text = "Health: " + hero.getCurrHealth() + "/" + hero.getHealth();
        atk.text = "Atk: " + hero.getCurrAttack() + "/" + hero.getAttack();
        def.text = "Def: " + hero.getCurrDef() + "/" + hero.getDefence();
    }

    public void CardSelected()
    {
        if (!multiSelect)
        {
            foreach(SelectableCard card in activeCards)
            {
                card.CardSelected(false);
            }
        }
    }
}
