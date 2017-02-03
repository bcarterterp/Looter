using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : CardLogic {

    private Character monster;
    private Item item;

    public void GenerateItem(int level)
    {
        if (Random.Range(0, 1) == 0)
        {
            monster = new Phantom(level);
        }
        else
        {
            NextStage();
        }
        item = new Item(level);
    }

    public void FightMonster(Hero hero)
    {
        if (hero.getCurrAttack() <= monster.getCurrDef() && monster.getCurrAttack() <= hero.getCurrDef())
        {
            hero.loseHealth(1);
            monster.loseHealth(1);
        }
        else
        {
            hero.fightCharacter(monster);
            monster.fightCharacter(hero);
        }

        if (monster.getCurrHealth() <= 0)
        {
            hero.gainExperience(monster.getExperience());
            hero.AdjustGold(monster.getGold());
            NextStage();
        }
    }

    public override bool IsLastStage()
    {
        return GetStage() == 1;
    }

    public Character GetMonster()
    {
        return monster;
    }

    public Item GetItem()
    {
        return item;
    }
}
