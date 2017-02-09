using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefLogic : CardLogic
{

    private Item stolenItem;
    private int stolenGold;

    public void Steal(Hero hero)
    {
        stolenItem = hero.removeRandomEquipment();
        if (stolenItem == null)
        {
            int gold = hero.getGold();
            if (hero.getGold() > 0)
            {
                stolenGold = 0;
                if (hero.getGold() > 10)
                {
                    stolenGold = gold / 10;
                }
                else
                {
                    stolenGold = 5;
                }
            }
        }
        ShouldProgress();
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoverText();
            case 1:
                return GetStolenItemText();
            default:
                return "What!?!?!?";
        }
    }

    private string GetDiscoverText()
    {
        ShouldProgress();
        return "You decide to take a break to rest a bit";
    }

    private string GetStolenItemText()
    {
        if (stolenItem != null)
        {
            return "It looks like your " + stolenItem.getType().ToString() + " was stolen";
        }
        else if (stolenGold > 0)
        {
            return "Your pockets feel lighter, " + stolenGold + " gold was stolem";
        }
        else
        {
            return "You feel utterly relaxed!";
        }
    }

    public override Item GetItem()
    {
        return stolenItem;
    }

    public override int GetGold()
    {
        return stolenGold;
    }
}
