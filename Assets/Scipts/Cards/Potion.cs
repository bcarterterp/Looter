using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Potion
{

    private const int TOTAL_OPTIONS = 8;

    public void drinkPotion(Hero hero)
    {
        int value = Random.Range(0,TOTAL_OPTIONS);
        switch (value)
        {
            case 0:
                hero.gainHealth(hero.getLevel() * 5);
                break;
            case 1:
                hero.loseHealth(hero.getLevel() * 5);
                break;
            case 2:
                hero.gainAttack(Random.Range(0, 3) + 1);
                break;
            case 3:
                hero.loseAttack(Random.Range(0, 2) + 1);
                break;
            case 4:
                hero.gainDef(Random.Range(0, 2) + 1);
                break;
            case 5:
                hero.loseDef(Random.Range(0, 2) + 1);
                break;
            case 6:
                hero.gainExperience(hero.getExpThreshhold() / 5);
                break;
            case 7:
                hero.refreshHero();
                break;
        }

    }
}