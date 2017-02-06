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
    private GameObject[] activeCards;
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
            if (card == 0)
            {
                activeCard.ShowMonsterCard(logic.GetMonster(), false);
            }
            else
            {
                activeCard.ShowCard(card, false);
            }
        }
        else
        {
            logic.InteractWithActiveCard();
            switch (logic.GetActiveCard())
            {
                case (int)CardType.MONSTER:
                    MonsterFlow();
                    break;
                case (int)CardType.POTION:
                    PotionFlow();
                    break;
                case (int)CardType.ITEM:
                    break;
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

    public void MonsterFlow()
    {
        if(logic.GetLogicStage() == 0)
        {
            activeCard.ShowMonsterCard(logic.GetMonster(), false);
        }
    }

    public void PotionFlow()
    {

    }

    public void ItemFlow()
    {

    }

    public void MerchantFlow()
    {
		if (activeCards == null) {
			ShowMerchantItems ();
		} else {
			OptionsSelected();
		}
    }

    public void ShowMerchantItems()
    {
        activeCard.HideCard();
        Item[] items = logic.GetItems();
        activeCards = new GameObject[items.Length];
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
            activeCards[i] = Instantiate(prefab);
            SelectableCard card = activeCards[i].GetComponent<SelectableCard>();
            card.transform.position = new Vector3 (offset, CARD_HEIGHT);
            card.transform.SetParent (canvas.transform, false);
            card.ShowItemCard(items[i], true);
		}
    }

	public void OptionsSelected(){
		for (int i = 0; i < activeCards.Length; i++) {
            SelectableCard card = activeCards[i].GetComponent<SelectableCard>();
            if (card.IsSelected()) {
				logic.InteractWithActiveCard (i);
                break;
			}
		}
		for (int i = 0; i < activeCards.Length; i++) {
			Destroy (activeCards [i]);
		}
		activeCards = null;
		activeCard.ShowCard (-1, false);
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
            foreach(GameObject cardObject in activeCards)
            {
                SelectableCard card = cardObject.GetComponent<SelectableCard>();
                card.CardSelected(false);
            }
        }
    }
}
