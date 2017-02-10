using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellLogic : CardLogic {

	public override void Interact (Hero hero)
	{
		RefreshHero (hero);
	}

    public void RefreshHero(Hero hero)
    {
        hero.refreshHero();
        ShouldProgress();
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoveryText();
            case 1:
                return GetRefreshedText();
            default:
                return "WHAT!?!?!";
        }
    }

    private string GetDiscoveryText()
    {
        ShouldProgress();
        return "You stumble upon a well";
    }

    private string GetRefreshedText()
    {
        return "You feel completely refreshed!";
    }
}
