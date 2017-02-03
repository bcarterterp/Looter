using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour
{
    private const float CARD_WIDTH = 400f;
    private const float CARD_SPACE = 20f;

    private AdventureLogic logic;

    public Text health, atk, def, storyText;
    public Image activeCard;
    public Image[] activeCards;
    public Sprite[] cardSprites;
    public Sprite cardBack;
    public Canvas canvas;

    private void Start()
    {
        logic = new AdventureLogic();
        logic.StartGame();
        UpdatePanel();
        activeCard.transform.position = new Vector3(0, 162.5f);
        activeCard = (Image)Instantiate(activeCard);
        activeCard.transform.SetParent(canvas.transform, false);
        ShowCard(-1);
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
            ShowCard(card);
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
                    ShowCard(-1);
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
				logic.InteractWithActiveCard(0);
				break;
		}
        //switch (logic.GetCardState())
        //{
        //    case 0:
        //        storyText.text = "Like my wares?";
        //        break;
        //    case 1:
        //        if (activeCards == null)
        //        {
        //            ShowMerchantItems();
        //        }
        //        else
        //        {
        //            activeCards = null;
        //            logic.InteractWithActiveCard(0);
        //        }
        //        break;
        //}
    }

    public void ShowMerchantItems()
    {
        Item[] items = logic.GetItems();
        activeCards = new Image[items.Length];
        float startingOffset = 0;
        switch (items.Length)
        {
            case 1:
                startingOffset = 1.5f * CARD_WIDTH + CARD_SPACE;
                break;
            case 2:
                startingOffset = CARD_WIDTH + .5f * CARD_SPACE;
                break;
            case 3:
                startingOffset = .5f * CARD_WIDTH;
                break;
        }

        for (int i = 0; i < items.Length; i++)
        {
            float offset = -startingOffset + i * (CARD_WIDTH + CARD_SPACE);
            activeCards[i] = (Image)Instantiate(activeCard);
            activeCards[i].transform.position = new Vector3(offset, 162.5f);
            activeCards[i].transform.SetParent(canvas.transform, false);
            activeCards[i].sprite = cardSprites[(int)CardType.ITEM];
        }
    }

    public void Decline()
    {
        logic.Decline();
        ShowCard(-1);
    }

    public void ClearGame()
    {
        logic.WipeAllData();
    }

    private void ShowCard(int card)
    {
        if (card == -1)
        {
            activeCard.sprite = cardBack;
        }
        else
        {
            activeCard.sprite = cardSprites[card];
        }
        Component[] texts = activeCard.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            text.enabled = false;
            if ((CardType)card == CardType.MONSTER)
            {
                text.text = ShowMonsterStats(text);
            }
            //else if ((CardType)card == CardType.ITEM)
            //{
            //    text.text = ShowItemStats(text);
            //}
        }
    }

    private string ShowMonsterStats(Text text)
    {
        text.enabled = true;
        string value = "";
        Character monster = logic.GetMonster();
        switch (text.name)
        {
            case "Health":
                value = "Health: " + monster.getCurrHealth() + "/" + monster.getHealth();
                break;
            case "Atk":
                value = "Atk: " + monster.getCurrAttack() + "/" + monster.getAttack();
                break;
            case "Def":
                value = "Def: " + monster.getCurrDef() + "/" + monster.getDefence();
                break;
        }
        return value;
    }

    private string ShowItemStats(Text text)
    {
        text.enabled = true;
        string value = "";
        //Item item = logic.GetItem();
        //switch (text.name)
        //{
        //    case "Health":
        //        value = "Health: " + item.getHealth();
        //        break;
        //    case "Atk":
        //        value = "Atk: " + item.getAttack();
        //        break;
        //    case "Def":
        //        value = "Def: " + item.getDef();
        //        break;
        //}
        return value;
    }

    private void UpdatePanel()
    {
        Hero hero = logic.GetHero();
        health.text = "Health: " + hero.getCurrHealth() + "/" + hero.getHealth();
        atk.text = "Atk: " + hero.getCurrAttack() + "/" + hero.getAttack();
        def.text = "Def: " + hero.getCurrDef() + "/" + hero.getDefence();
    }

}
