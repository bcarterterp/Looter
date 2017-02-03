using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : Character {

    public Phantom(int level)
    {
        setLevel(level);
        initHealth();
        initAttack();
        initDefence();
        initGold();
        initExp();
        SetEvasionChance(10);
    }

    private void initHealth()
    {
        int health = 1;
        if(getLevel() > 1)
        {
            health += getLevel() / 2;
        }
        setHealth(health);
        setCurrHealth(getHealth());
    }

    private void initAttack()
    {
        setAttack(getLevel() + 1);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        int def = 0;
        if(getLevel() > 1)
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
}