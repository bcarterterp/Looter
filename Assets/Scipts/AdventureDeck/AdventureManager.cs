using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour
{
	private const float CARD_WIDTH = 160f;
	private const float CARD_HEIGHT = 100f;
    private const float CARD_SPACE = 40f;

    private AdventureLogic logic;

    public Text health, atk, def, storyText;
	public Button prefab;
    public Button activeCard;
    public Button[] activeCards;
    public Sprite[] cardSprites;
    public Sprite cardBack;
    public Canvas canvas;

    private void Start()
    {
        logic = new AdventureLogic();
        logic.StartGame();
        UpdatePanel();
		activeCard = (Button)Instantiate(prefab);
        activeCard.transform.position = new Vector3(0, 100f);
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
    }

    public void ShowMerchantItems()
    {
		activeCard.image.enabled = false;
        Item[] items = logic.GetItems();
        activeCards = new Button[items.Length];
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
		Debug.Log (items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            float offset = -startingOffset + i * (CARD_WIDTH + CARD_SPACE);
			Debug.Log (offset);
			activeCards[i] = (Button)Instantiate(prefab);
			activeCards[i].transform.position = new Vector3(offset, CARD_HEIGHT);
            activeCards[i].transform.SetParent(canvas.transform, false);
			activeCards[i].image.sprite = cardSprites[(int)CardType.ITEM];
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
		Image cardImage = activeCard.image;
        if (card == -1)
        {
			cardImage.sprite = cardBack;
        }
        else
        {
			cardImage.sprite = cardSprites[card];
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

	public void SetCardImage(Image image, int card){

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
