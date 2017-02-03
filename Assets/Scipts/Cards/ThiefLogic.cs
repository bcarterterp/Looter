using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefLogic : CardLogic {

	public void StealItem(Hero hero)
    {
        hero.removeRandomEquipment();
    }

}
