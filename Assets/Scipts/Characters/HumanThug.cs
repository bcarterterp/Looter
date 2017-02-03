using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanThug : Character {

    public HumanThug(int level)
    {
        setLevel(level);
        initHealth();
        initAttack();
        initDefence();
        initGold();
        initExp();
    }

    private void initHealth()
    {
        setHealth(getLevel()+1);
        setCurrHealth(getHealth());
    }

    private void initAttack()
    {
        setAttack(getLevel() + 1);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        setDefence(getLevel());
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
