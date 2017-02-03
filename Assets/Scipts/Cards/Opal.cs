using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opal{

	public void RubOpal(int choice, Hero hero)
    {
        switch (choice)
        {
            case 0:
                hero.setHealth(hero.getHealth() + 1);
                hero.setCurrHealth(hero.getCurrHealth() + 1);
                break;
            case 1:
                hero.setDefence(hero.getDefence() + 1);
                hero.setCurrDef(hero.getCurrDef() + 1);
                break;
            case 2:
                hero.setAttack(hero.getAttack() + 1);
                hero.setCurrAttack(hero.getCurrAttack() + 1);
                break;
        }
    }

}