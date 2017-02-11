using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerLogic : CardLogic{

	private bool guessedCorrectly;
	private int gold;

	public override void Interact (Hero hero, int option)
	{
		PlayShellGame (hero, option);
	}

	private void PlayShellGame(Hero hero, int option){
		guessedCorrectly = Random.Range (0, 3) == option;
		if (guessedCorrectly) {
			gold = 50 * hero.getLevel ();
		} else {
			gold = -50;
		}
		hero.AdjustGold (gold);
		ShouldProgress ();
	}

	public override string GetStoryText()
	{
		switch (GetStage ()) {
		case 0:
			return GetDiscoveryText ();
		case 1:
			return GetSelectOptionText ();
		case 2:
			return GetResultsText();
		default:
			return "WHAT!?!?!";
		}
	}

	private string GetDiscoveryText(){
		ShouldProgress ();
		return "Hello traveler, mlet's play a gmae of shells!";
	}

	private string GetSelectOptionText(){
		ShouldProgress ();
		return "Select one, may luck be on your side...";
	}

	private string GetResultsText(){
		if (guessedCorrectly) {
			return "Lady Luck has smiled upon you! You one" + gold + "gold";
		} else {
			return "Maybe next time traveler!";
		}
	}

	public override bool IsLastStage ()
	{
		return GetStage () == 2;
	}

}
