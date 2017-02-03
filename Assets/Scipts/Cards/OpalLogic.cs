using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpalLogic : CardLogic {

    Opal opal = new Opal();

    public void RubOpal(Hero hero, int choice)
    {
        opal.RubOpal(choice, hero);
    }

}
