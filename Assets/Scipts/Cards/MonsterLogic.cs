using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : CardLogic{

    public Character monster;

    public void EncounterRandomMonster(int level)
    {
        monster = new HumanThug(level);
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

    public Character GetMonster()
    {
        return monster;
    }

    public override bool IsLastStage()
    {
        return GetStage() == 1;
    }
}
