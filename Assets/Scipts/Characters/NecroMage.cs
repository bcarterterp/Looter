using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroMage : Character {

    private int skeletonSummonPhase;

    public NecroMage(int level)
    {
        setLevel(level);
        initHealth();
        initAttack();
        initDefence();
        initGold();
        initExp();
        skeletonSummonPhase = 0;
    }

    public override void fightCharacter(Hero character)
    {
        base.fightCharacter(character);
        skeletonSummonPhase++;
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
            atk = getLevel() / 2;
        }
        setAttack(atk);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        int def = 0;
        if (getLevel() > 1)
        {
            def += getLevel() / 2;
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

    public bool CanSummonSkeleton()
    {
        return skeletonSummonPhase % 3 == 0;
    }
}