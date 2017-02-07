using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hero : Character
{

    Dictionary<Item.ItemType, Item> inventory;

    public Hero() : base()
    {
        inventory = new Dictionary<Item.ItemType, Item>();
        setHealth(15);
        setCurrHealth(getHealth());
        setAttack(2);
        setCurrAttack(getAttack());
        setDefence(1);
        setCurrDef(getDefence());
    }

    public bool gainExperience(int experience)
    {
        bool levelUp = false;
        experience += getExperience();
        if (experience >= getExpThreshhold())
        {
            experience -= getExpThreshhold();
            setLevel(getLevel() + 1);
            levelUp = true;
        }
        setExperience(experience);
        return levelUp;
    }

    public void AdjustGold(int amount)
    {
        setGold(getGold() + amount);
    }

    public void refreshHero()
    {
        setCurrHealth(getHealth());
        setCurrAttack(getAttack());
        setCurrDef(getDefence());
    }

    public void AddEquipment(Item item)
    {
        inventory.Add(item.getType(), item);
    }

    public bool hasEquipment(Item.ItemType type)
    {
        return inventory.ContainsKey(type);
    }

    public void AquireEquipment(Item item)
    {
        removeEquipment(item.getType());
        inventory.Add(item.getType(), item);
        setHealth(getHealth() + item.getHealth());
        gainHealth(item.getHealth());
        setAttack(getAttack() + item.getAttack());
        gainAttack(item.getAttack());
        setDefence(getDefence() + item.getDef());
        gainDef(item.getDef());
    }

    public Item removeRandomEquipment()
    {
        Item item = GetRandomArmor();
        if (item != null)
        {
            Item.ItemType type = GetRandomArmor().getType();
            removeEquipment(type);
        }
        return item;
    }

    public Item removeEquipment(Item.ItemType type)
    {
        Item item = GetItem(type);
        if (item != null)
        {
            inventory.Remove(type);
            setHealth(getHealth() - item.getHealth());
            loseHealth(item.getHealth());
            setAttack(getAttack() - item.getAttack());
            loseAttack(item.getAttack());
            setDefence(getDefence() - item.getDef());
            loseDef(item.getDef());
        }
        return item;
    }

    public Item GetItem(Item.ItemType type)
    {
        Item item = null;
        if (inventory.TryGetValue(type, out item))
        {
            return item;
        }
        else
        {
            return null;
        }
    }

    public Item GetRandomArmor()
    {
        if(inventory.Count > 0)
        {
            List<Item> values = Enumerable.ToList(inventory.Values);
            return values[Random.Range(0, inventory.Count)];
        }
        return null;
    }
}
