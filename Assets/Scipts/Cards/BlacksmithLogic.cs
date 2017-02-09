using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithLogic : CardLogic {

    private Blacksmith blacksmith = new Blacksmith();
    private Item[] items;
    private Item reforgedItem;

    public void SetReforgeableItems(Hero hero)
    {
        items = blacksmith.ListOfReforgables(hero);
        ShouldProgress();
    }

    public void ReforgeItem(Hero hero, int choice)
    {
        Item.ItemType type = items[choice].getType();
        reforgedItem = new Item(hero.getLevel());
        reforgedItem.setType(type);
        hero.AdjustGold(-50);
        hero.AquireEquipment(reforgedItem);
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
                return GetFinishedReforgingText();
            case 3:
                return GetGoodByeText();
            default:
                return "What!?!?!?";
        }
    }

    private string GetDiscoverText()
    {
        ShouldProgress();
        return "Need something repurposed?";
    }

    private string GetShowOptionsText()
    {
        ShouldProgress();
        return "Choose which to turn from junk to treasure!";
    }

    private string GetFinishedReforgingText()
    {
        ShouldProgress();
        return "All done, I hope it suits your purpose!";
    }

    private string GetGoodByeText()
    {
        return "Until we meet again traveler!";
    }

    public override bool IsLastStage()
    {
        return GetStage() == 3;
    }

    public override Item GetItem()
    {
        return reforgedItem;
    }

    public override Item[] GetItems()
    {
        return items;
    }

}
