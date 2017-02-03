using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLogic : CardLogic {

    private Potion potion;

	public void DrinkPotion(Hero hero)
    {
        potion.drinkPotion(hero);
    }

}
