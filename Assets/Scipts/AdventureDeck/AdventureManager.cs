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
    private AdventureStoryGenerator storyGenerator;
    private SelectableCard activeCard;
    private GameObject[] activeCards;
    private bool[] optionsSelected;
    private bool multiSelect;
    private int stage;

    public Text health, atk, def, storyText;
	public GameObject prefab;
    public Canvas canvas;

    private void Start()
    {
        logic = new AdventureLogic();
        storyGenerator = new AdventureStoryGenerator();
        logic.StartGame();
        UpdatePanel();
		activeCard = Instantiate(prefab).GetComponent<SelectableCard>();
        activeCard.transform.position = new Vector3(0, 100f);
        activeCard.transform.SetParent(canvas.transform, false);
        activeCard.ShowCard(-1, false);
        multiSelect = false;
        stage = 0;
    }

    public void ProgressAdventure()
    {
        switch (stage)
        {
            case (int)AdventureStage.TRANSITION:
                TransitionStage();
                break;
            case (int)AdventureStage.DISCOVERY:
                DiscoveryStage();
                break;
            case (int)AdventureStage.INTERACTION:
                InteractionStage();
                break;
            case (int)AdventureStage.COMPLETION:
                CompletionStage();
                break;
            case (int)AdventureStage.ARRIVAL:
                ArrivalStage();
                break;
        }
    }

    private void AvoidCard()
    {
        stage = 0;
        storyText.text = storyGenerator.GetAvoidText(logic.GetActiveCard());
    }

    private void TransitionStage()
    {
        storyText.text = storyGenerator.GetAdventureTransitionText();
        stage++;
    }

    private void DiscoveryStage()
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
        storyText.text = storyGenerator.GetCardDiscoveryText(card);
        stage++;
    }

    private void InteractionStage()
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
                stage++;
                break;
        }
        UpdatePanel();
        if (logic.GetHero().getCurrHealth() == 0)
        {
            logic.WipeAllData();
        }
    }

    private void CompletionStage()
    {
        storyText.text = storyGenerator.GetCompletionText((logic.GetActiveCard()));
        if (logic.AdventureComplete())
        {
            stage++;
        }
        else
        {
            stage = 0;
        }
    }

    private void ArrivalStage()
    {
        storyText.text = storyGenerator.GetArrivalText();
        logic.Restart();
        stage = 0;
    }

    private void MonsterFlow()
    {
        if(logic.GetLogicStage() == 0)
        {
            activeCard.ShowMonsterCard(logic.GetMonster(), false);
        }
    }

    private void PotionFlow()
    {

    }

    private void ItemFlow()
    {

    }

    private void MerchantFlow()
    {
		if (activeCards == null) {
			ShowMerchantItems ();
		} else {
			OptionsSelected();
		}
    }

    private void ShowMerchantItems()
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

    private void OptionsSelected(){
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

    private void Decline()
    {
        logic.Decline();
        activeCard.ShowCard(-1, false);
    }

    private void ClearGame()
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
