using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuedLogic : CardLogic {

    private Item item;
    private int gold;

    public RescuedLogic()
    {
        if(Random.Range(0,2) == 0)
        {
            item = new Item();
        }
        else
        {
            item = null;
            gold = Random.Range(0, 70) + 10;
        }
    }

    public override void Interact(Hero hero)
    {
        if(GetStage() == 1 && item != null)
        {
            hero.AquireEquipment(item);
        }
        ShouldProgress();
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoveryText();
            case 1:
                return GetRewardText();
            default:
                return "WHAT!?!?!?";
        }
    }

    private string GetDiscoveryText()
    {
        ShouldProgress();
        return "Thank you kindly!";
    }

    private string GetRewardText()
    {
        return "Here's a reward for all your help!";
    }

    public override Item GetItem()
    {
        return item;
    }

    public override int GetGold()
    {
        return gold;
    }
}
