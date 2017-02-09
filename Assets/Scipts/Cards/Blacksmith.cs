using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith{

    private List<Item.ItemType> proficiency;

    public Blacksmith()
    {
        proficiency = new List<Item.ItemType>();
    }

    public void SetNewProficiencies()
    {
        proficiency = new List<Item.ItemType>();
        while (proficiency.Count < 2)
        {
            int possible = Random.Range(0, (int)Item.ItemType.TOTAL_SLOTS - 1);
            Item.ItemType type = (Item.ItemType)possible;
            if (!proficiency.Contains(type))
            {
                proficiency.Add(type);
            }
        }
    }

    public Item[] ListOfReforgables(Hero hero)
    {
        return hero.GetItems();
    }

    public Item[] ListOfReforgablesWithProficiency(Hero hero)
    {
        List<Item> items = new List<Item>();
        foreach(Item.ItemType type in proficiency)
        {
            Item item = hero.GetItem(type);
            if(item != null)
            {
                items.Add(item);
            }
        }
        return items.ToArray();
    }

    public void ReforgeItem(Item item, int level)
    {
        Item newItem = new Item(level);
        newItem.setType(item.getType());
        item = newItem;
    }
}
