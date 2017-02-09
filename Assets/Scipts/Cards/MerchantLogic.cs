using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantLogic : CardLogic
{

    Merchant merchant = new Merchant();
    Item[] items;

    public void GenerateItems(int level)
    {
        items = merchant.GetMerchantItems(level);
        ShouldProgress();
    }

    public void BuyItem(Hero hero, int choice)
    {
        hero.AdjustGold(-items[choice].getPrice());
        hero.AquireEquipment(items[choice]);
        ShouldProgress();
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoverText();
            case 1:
                return GetShowOptionsText();
            case 2:
                return GetGoodByeText();
            default:
                return "What!?!?!?";
        }
    }

    public string GetDiscoverText()
    {
        ShouldProgress();
        return "Hello, Adventurer! How can I help you?";
    }

    public string GetShowOptionsText()
    {
        ShouldProgress();
        return "Come, and ponder my wares!";
    }

    public string GetGoodByeText()
    {
        return "Good choice adventurer, until next time!";
    }

    public override bool IsLastStage()
    {
        return GetStage() == 2;
    }

    public override Item[] GetItems()
    {
        return items;
    }
}
