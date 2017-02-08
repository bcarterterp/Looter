using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpalLogic : CardLogic {

    Opal opal = new Opal();

    public void RubOpal(Hero hero, int choice)
    {
        opal.RubOpal(choice, hero);
		ShouldProgress ();
    }

	public override string GetStoryText(){
		switch (GetStage ()) {
		case 0:
			return GetDiscoverText();
		case 1:
			return GetShowOptionsText ();
		case 2:
			return GetGoodByeText ();
		default:
			return "What!?!?!?";
		}
	}

	public string GetDiscoverText(){
		ShouldProgress();
		return "I am an opal that can give you power!";
	}

	public string GetShowOptionsText(){
		ShouldProgress ();
		return "Which blessing would you like?";
	}

	public string GetGoodByeText(){
		return "The opal vanishes making you stronger!";
	}

	public override bool IsLastStage()
	{
		return GetStage() == 2;
	}
}
