using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{

    private int health;
    private int currHealth;
    private int attack;
    private int currAttack;
    private int defence;
    private int currDef;
    private int level;
    private int gold;
    private int experience;
    private int evasionChance;

    public Character()
    {
        health = 1;
        attack = 1;
        defence = 1;
        level = 1;
        gold = 0;
        experience = 0;
        evasionChance = 5;
    }

    public virtual void fightCharacter(Hero character)
    {
        int damage = getCurrAttack() - character.getCurrDef();
        if (damage > 0 && Random.Range(1,100) > character.GetEvasionChance())
        {
            character.setCurrHealth(character.getCurrHealth() - damage);
        }
    }

    public void fightCharacter(Character character)
    {
        int damage = getCurrAttack() - character.getCurrDef();
        if (damage > 0 && Random.Range(1, 100) > character.GetEvasionChance())
        {
            character.setCurrHealth(character.getCurrHealth() - damage);
        }
    }

    public int getExpThreshhold()
    {
        return level * level + 5;
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getCurrHealth()
    {
        return currHealth;
    }

    public void setCurrHealth(int currHealth)
    {
        this.currHealth = currHealth;
    }

    public void gainHealth(int amount)
    {
        currHealth += amount;
        if (currHealth >= health)
        {
            currHealth = health;
        }
    }

    public void loseHealth(int amount)
    {
        currHealth -= amount;
        if (currHealth <= 0)
        {
            currHealth = 0;
        }
    }

    public int getAttack()
    {
        return attack;
    }

    public void setAttack(int attack)
    {
        this.attack = attack;
    }

    public int getCurrAttack()
    {
        return currAttack;
    }

    public void setCurrAttack(int currAttack)
    {
        this.currAttack = currAttack;
    }

    public void gainAttack(int amount)
    {
        currAttack += amount;
        if (currAttack >= attack)
        {
            currAttack = attack;
        }
    }

    public void loseAttack(int amount)
    {
        currAttack -= amount;
        if (currAttack <= 0)
        {
            currAttack = 0;
        }
    }

    public int getDefence()
    {
        return defence;
    }

    public void setDefence(int defence)
    {
        this.defence = defence;
    }

    public int getCurrDef()
    {
        return currDef;
    }

    public void setCurrDef(int currDef)
    {
        this.currDef = currDef;
    }

    public void gainDef(int amount)
    {
        currDef += amount;
        if (currDef >= defence)
        {
            currDef = defence;
        }
    }

    public void loseDef(int amount)
    {
        currDef -= amount;
        if (currDef <= 0)
        {
            currDef = 0;
        }
    }

    public int getLevel()
    {
        return level;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public int getGold()
    {
        return gold;
    }

    public void setGold(int gold)
    {
        this.gold = gold;
    }

    public int getExperience()
    {
        return experience;
    }

    public void setExperience(int experience)
    {
        this.experience = experience;
    }

    public void SetEvasionChance(int evasion)
    {
        evasionChance = evasion;
    }

    public int GetEvasionChance()
    {
        return evasionChance;
    }
}
