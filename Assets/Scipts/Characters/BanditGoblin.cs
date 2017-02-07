using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditGoblin : Character {

    public BanditGoblin(int level)
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
        setHealth(getLevel() + 1);
        setCurrHealth(getHealth());
    }

    private void initAttack()
    {
        int atk = 2;
        if(getLevel() > 1)
        {
            atk = getLevel() * 2 / 3;
        }
        setAttack(atk);
        setCurrAttack(getAttack());
    }

    private void initDefence()
    {
        int def = 0;
        if(getLevel() > 1)
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

    public override int fightCharacter(Hero hero)
    {
        int ignoredValue = 0;
        Item ignoredItem = hero.GetRandomArmor();
        if (ignoredItem != null)
        {
            ignoredValue = ignoredItem.getDef();
        }
        int damage = getCurrAttack() - hero.getCurrDef() + ignoredValue;
        if (damage > 0)
        {
            hero.TakeDamage(damage);
        }
        return damage;
    }
}
