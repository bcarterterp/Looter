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
        activeCard.HideCard();
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
            case (int)AdventureStage.ARRIVAL:
                ArrivalStage();
                break;
        }
        logic.StageCheck();
    }

    private void AvoidCard()
    {
        stage = 0;
        storyText.text = storyGenerator.GetAvoidText(logic.GetActiveCard());
        activeCard.HideCard();
    }

    private void TransitionStage()
    {
        activeCard.HideCard();
        storyText.text = storyGenerator.GetAdventureTransitionText();
        stage++;
    }

    private void DiscoveryStage()
    {
        int card = logic.DrawCard();
        if (card == 0)
        {
            activeCard.ShowCard(logic.GetMonster(), false);
        }
        else
        {
            activeCard.ShowCard(card, false);
        }

        storyText.text = logic.GetCardText();
        stage++;
    }

    private void InteractionStage()
    {
        int cardType = logic.GetActiveCard();
        logic.InteractWithActiveCard();
        switch (cardType)
        {
            case (int)CardType.MONSTER:
                MonsterFlow();
                break;
            case (int)CardType.POTION:
                PotionFlow();
                break;
            case (int)CardType.ITEM:
                ItemFlow();
                break;
            case (int)CardType.OPAL:
                OpalFlow();
                break;
            case (int)CardType.MERCHANT:
                MerchantFlow();
                break;
            case (int)CardType.BLACKSMITH:
                BlacksmithFlow();
                break;
            case (int)CardType.THIEF:
                ThiefFlow();
                break;
            case (int)CardType.WELL:
                WellFlow();
                break;
            case (int)CardType.GRAVE:
                GraveFlow();
                break;
            case (int)CardType.GAMBLER:
                GamblerFlow();
                break;
            case (int)CardType.PASSERBY:
                PasserbyFlow();
                break;
            case (int)CardType.RESCUED:
                RescuedFlow();
                break;
        }
        storyText.text = logic.GetCardText();
        UpdatePanel();
        if (logic.IsLastStage())
        {
            if (logic.AdventureComplete())
            {
                stage++;
            }
            else
            {
                stage = 0;
            }
        }
        if (logic.GetHero().getCurrHealth() == 0)
        {
            logic.WipeAllData();
        }
    }

    private void ArrivalStage()
    {
        DestroyCreatedCards();
        activeCard.HideCard();
        storyText.text = storyGenerator.GetArrivalText();
        logic.Restart();
        stage = 0;
    }

    private void MonsterFlow()
    {
        activeCard.ShowCard(logic.GetMonster(), false);
    }

    private void PotionFlow()
    {

    }

    private void ItemFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            if (logic.GetMonster() != null)
            {
                activeCard.ShowCard(logic.GetMonster(), false);
            }
            else
            {
                activeCard.ShowCard(logic.GetItem(), false);
            }
        }
        else if (logic.GetLogicStage() == 2)
        {
            if (logic.GetMonster() != null)
            {
                activeCard.ShowCard(logic.GetMonster(), false);
            }
            else
            {
                activeCard.ShowCard(logic.GetItem(), false);
            }
        }
    }

    private void OpalFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            ShowOpalBlessings();
        }
        else if (logic.GetLogicStage() == 2)
        {
            OptionsSelectedAndDestroy(logic.GetActiveCard());
        }
    }

    private void ShowOpalBlessings()
    {
        object[] blessingCards = new object[3];
        blessingCards[0] = -1;
        blessingCards[1] = -1;
        blessingCards[2] = -1;
        ShowUserChoices(blessingCards);
    }

    private void MerchantFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            ShowMerchantItems();
        }
        else if (logic.GetLogicStage() == 2)
        {
            OptionsSelectedAndDestroy(logic.GetActiveCard());
        }
    }

    private void ShowMerchantItems()
    {
        Item[] items = logic.GetItems();
        ShowUserChoices(items);
    }

    private void BlacksmithFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            ShowMerchantItems();
        }
        else if (logic.GetLogicStage() == 2)
        {
            OptionsSelectedAndDestroy(null);
            activeCard.ShowCard(logic.GetItem(), false);
        }
        else if (logic.GetLogicStage() == 3)
        {
            activeCard.ShowCard(logic.GetActiveCard(), false);
        }
    }

    private void ShowBlackSmithReforgables()
    {
        Item[] items = logic.GetItems();
        ShowUserChoices(items);
    }

    private void ThiefFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            Item item = logic.GetItem();
            if (item != null)
            {
                activeCard.ShowCard(item, false);
            }
            else if (logic.GetGold() > 0)
            {
                activeCard.ShowCard(-1, false);
            }
            else
            {
                activeCard.ShowCard(-1, false);
            }
        }
    }

    private void WellFlow()
    {

    }

    private void GraveFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            ShowGraveOptions();
        }
        else if (logic.GetLogicStage() == 2)
        {
            OptionsSelectedAndDestroy(null);
            if (logic.GetMonster() != null)
            {
                activeCard.ShowCard(logic.GetMonster(), false);
            }
            else if (logic.GetItem() != null)
            {
                activeCard.ShowCard(logic.GetItem(), false);
            }
            else
            {
                activeCard.ShowCard(-1, false);
            }
        }
        else if (logic.GetLogicStage() == 3)
        {
            if (logic.GetMonster() != null)
            {
                activeCard.ShowCard(logic.GetMonster(), false);
            }
            else
            {
                if (logic.GetItem() != null)
                {
                    activeCard.ShowCard(logic.GetItem(), false);
                }
                else
                {
                    activeCard.ShowCard(-1, false);
                }
            }
        }
    }

    private void ShowGraveOptions()
    {
        object[] graveOptions = new object[2];
        graveOptions[0] = -1;
        graveOptions[1] = -1;
        ShowUserChoices(graveOptions);
    }

    private void GamblerFlow()
    {
        if (logic.GetLogicStage() == 1)
        {
            ShowShellGame();
        }
        else if (logic.GetLogicStage() == 2)
        {
            OptionsSelected();
            ShowSelectedCard();
        }
    }

    private void ShowShellGame()
    {
        object[] shellChoices = new object[3];
        shellChoices[0] = -1;
        shellChoices[1] = -1;
        shellChoices[2] = -1;
        ShowUserChoices(shellChoices);
    }

    private void PasserbyFlow()
    {
        
    }

    private void RescuedFlow()
    {
        if(logic.GetLogicStage() == 1)
        {
            if(logic.GetItem() != null)
            {
                activeCard.ShowCard(logic.GetItem(), false);
            }
            else
            {
                activeCard.ShowCard(-1, false);
            }
        }
    }

    private void ShowSelectedCard()
    {
        for (int i = 0; i < activeCards.Length; i++)
        {
            SelectableCard card = activeCards[i].GetComponent<SelectableCard>();
            if (card.IsSelected())
            {
                card.ShowCard((int)CardType.OPAL, false);
                break;
            }
        }
    }

    private void ShowUserChoices(object[] dataObjects)
    {
        activeCard.HideCard();
        activeCards = new GameObject[dataObjects.Length];
        optionsSelected = new bool[dataObjects.Length];
        float startingOffset = 0;
        switch (dataObjects.Length)
        {
            case 2:
                startingOffset = .5f * (CARD_WIDTH + CARD_SPACE);
                break;
            case 3:
                startingOffset = CARD_WIDTH + CARD_SPACE;
                break;
        }
        for (int i = 0; i < dataObjects.Length; i++)
        {
            optionsSelected[i] = false;
            float offset = -startingOffset + i * (CARD_WIDTH + CARD_SPACE);
            activeCards[i] = Instantiate(prefab);
            SelectableCard card = activeCards[i].GetComponent<SelectableCard>();
            card.transform.position = new Vector3(offset, CARD_HEIGHT);
            card.transform.SetParent(canvas.transform, false);
            card.ShowCard(dataObjects[i], true);
        }
    }

    private void OptionsSelectedAndDestroy(object focusCard)
    {
        CardSelected();
        DestroyCreatedCards();
        activeCards = null;
        if (focusCard != null)
        {
            activeCard.ShowCard(focusCard, false);
        }
    }

    private void OptionsSelected()
    {
        for (int i = 0; i < activeCards.Length; i++)
        {
            SelectableCard card = activeCards[i].GetComponent<SelectableCard>();
            if (card.IsSelected())
            {
                logic.InteractWithActiveCard(i);
                break;
            }
        }
    }

    private void DestroyCreatedCards()
    {
        if (activeCards != null)
        {
            for (int i = 0; i < activeCards.Length; i++)
            {
                Destroy(activeCards[i]);
            }
        }
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
            foreach (GameObject cardObject in activeCards)
            {
                SelectableCard card = cardObject.GetComponent<SelectableCard>();
                card.CardSelected(false);
            }
        }
    }
}
