using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCard : MonoBehaviour {

	public Button button;
	public Text top, middle, bottom;
	public Sprite[] cardSprites;
	public Sprite cardBack;

    private CardSelectedListener listener;
    private bool selected;

    public void SetSelectedListener(CardSelectedListener cardListener)
    {
        listener = cardListener;
    }

	public void ShowMonsterCard(Character monster, bool enabled)
    {
        ShowCard(0, enabled);
        top.enabled = true;
        top.text = "Health: " + monster.getCurrHealth() + "/" + monster.getHealth();
        middle.enabled = true;
        middle.text = "Atk: " + monster.getCurrAttack() + "/" + monster.getAttack();
        bottom.enabled = true;
        bottom.text = "Def: " + monster.getCurrDef() + "/" + monster.getDefence();
    }

    public void ShowItemCard(Item item, bool enabled)
    {
        ShowCard(3, enabled);
        top.enabled = true;
        top.text = "Health: " + item.getHealth();
        middle.enabled = true;
        middle.text = "Atk: " + item.getAttack();
        bottom.enabled = true;
        bottom.text = "Def: " + item.getDef();
    }

    public void ShowCard(int cardType, bool enabled)
    {
        button.enabled = enabled;
        top.enabled = false;
        middle.enabled = false;
        bottom.enabled = false;
        if(cardType == -1)
        {
            button.image.sprite = cardBack;
        }
        else
        {
            button.image.sprite = cardSprites[cardType];
        }
    }

    public void HideCard()
    {
        button.enabled = false;
        button.image.enabled = false;
    }

    public void CardSelected()
    {
        if (!selected && listener != null)
        {
            listener.CardSelected();
            selected = true;
        }else {
            selected = !selected;
        }
    }

    public void CardSelected(bool on)
    {
        selected = on;
    }

    public bool IsSelected()
    {
        return selected;
    }

}
