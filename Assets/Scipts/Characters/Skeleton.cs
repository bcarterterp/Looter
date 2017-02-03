using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Character {

    private int resurectionChance;

    public Skeleton(int level)
    {
        setLevel(level);
        initHealth();
        initAttack();
        initDefence();
        initGold();
        initExp();
        resurectionChance = 10;
    }

    private void initHealth()
    {
        int health = 1;
        if (getLevel() > 1)
        {
            health += getLevel() / 3;
        }
        setHealth(health);
        setCurrHealth(getHealth());
    }

    private void initAttack()
    {
        int atk = 2;
        if (getLevel() > 1)
        {
            atk = getLevel() / 3;
        }
        setAttack(atk);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        int def = 0;
        if (getLevel() > 1)
        {
            def += getLevel() / 3;
        }
        setDefence(def);
        setCurrDef(getDefence());
    }

    private void initGold()
    {
        setGold(getLevel() * 2);
    }

    private void initExp()
    {
        setExperience(getLevel() * 2);
    }

    private int GetResurectionChance()
    {
        return resurectionChance;
    }
}