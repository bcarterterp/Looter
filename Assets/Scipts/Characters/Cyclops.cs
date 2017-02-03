using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : Character {

    bool canAttack;

    public Cyclops(int level)
    {
        setLevel(level);
        initHealth();
        initAttack();
        initDefence();
        initGold();
        initExp();
        canAttack = true;
    }

    public override void fightCharacter(Hero character)
    {
        if (canAttack)
        {
            base.fightCharacter(character);
        }
        canAttack = !canAttack;
    }

    private void initHealth()
    {
        int health = 4;
        if (getLevel() > 1)
        {
            health += getLevel() * 2 / 3;
        }
        setHealth(health);
        setCurrHealth(getHealth());
    }

    private void initAttack()
    {
        int atk = 4;
        if(getLevel() > 1)
        {
            atk += getLevel() * 2;
        }
        setAttack(atk);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        setDefence(0);
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