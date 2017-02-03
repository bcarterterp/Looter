using UnityEngine;

public class Item
{

    public enum ItemType
    {
        ARMOR,
        WEAPON_ONE,
        WEAPON_TWO,
        RELIC,
        TOTAL_SLOTS
    }

    private ItemType type;
    private int health;
    private int attack;
    private int def;
    private int price;

    public Item() { }

    public Item(int level)
    {
        int choice = Random.Range(0, (int)ItemType.TOTAL_SLOTS);
        type = (ItemType)choice;
        decideSpecs(level);
    }

    public Item(ItemType itemType, int health, int attack, int def, int price)
    {
        setType(type);
        setHealth(health);
        setAttack(attack);
        setDef(def);
        setPrice(price);
    }

    private void decideSpecs(int level)
    {
        int[] specs = new int[3];
        specs[Random.Range(0, 3)] = 1;
        for (int i = 1; i < specs.Length; i++)
        {
            specs[Random.Range(0, 3)] = 1;
        }
        if (specs[0] == 1)
        {
            initHealth(level);
        }
        if (specs[1] == 1)
        {
            initAttack(level);
        }
        if (specs[2] == 1)
        {
            initDef(level);
        }
        initPrice(level);
    }

    private void initHealth(int level)
    {
        health = Random.Range(1, (level / 2) + 6);
    }

    private void initAttack(int level)
    {
        attack = Random.Range(1,(level / 2) + 1);
    }

    private void initDef(int level)
    {
        def = Random.Range(1,(level / 2) + 1);
    }

    private void initPrice(int level)
    {
        price = (25 * health) + (25 * attack) + (25 * def);
    }

    public ItemType getType()
    {
        return type;
    }

    public void setType(ItemType type)
    {
        this.type = type;
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getAttack()
    {
        return attack;
    }

    public void setAttack(int attack)
    {
        this.attack = attack;
    }

    public int getDef()
    {
        return def;
    }

    public void setDef(int def)
    {
        this.def = def;
    }

    public int getPrice()
    {
        return price;
    }

    public void setPrice(int price)
    {
        this.price = price;
    }

}