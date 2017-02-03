using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithLogic : CardLogic {

    private Blacksmith blacksmith;
    private Item[] items;

    public BlacksmithLogic()
    {
        blacksmith = new Blacksmith();
    }

    public void SetReforgeableItems(Hero hero)
    {
        blacksmith.SetNewProficiencies();
        NextStage();
        items = blacksmith.ListOfReforgables(hero);
    }

    public void ReforgeItem(Hero hero, int choice)
    {
        Item.ItemType type = items[choice].getType();
        Item item = new Item(hero.getLevel());
        item.setType(type);
        hero.AdjustGold(-50);
        hero.AquireEquipment(item);
    }

    public override bool IsLastStage()
    {
        return GetStage() == 2;
    }

    public Item[] GetItems()
    {
        return items;
    }

}
