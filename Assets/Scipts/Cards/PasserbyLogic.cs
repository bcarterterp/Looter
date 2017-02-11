using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasserbyLogic : CardLogic {

	private AdventureStack stack;
	private int numberOfMonsters;

	public PasserbyLogic(AdventureStack adventureStack){
		stack = adventureStack;
	}

	public override void Interact (Hero hero)
	{
		if (GetStage () == 1) {
			PopulateAdventureStack ();
		} else {
			ShouldProgress ();
		}
	}

	public void PopulateAdventureStack(){
		stack.Push ((int)CardType.RESCUED);
		numberOfMonsters = Random.Range (0, 2)+2;
		for (int i = 0; i < numberOfMonsters; i++) {
			stack.Push ((int)CardType.MONSTER);
		}
	}

	public override string GetStoryText ()
	{
		switch (GetStage ()) {
		case 0:
			return GetDiscoveryText ();
		case 1:
			return GetAcceptedText ();
		default:
			return "What!?!?!";
		}
	}

	private string GetDiscoveryText(){
        ShouldProgress();
        return "Help! My father has been kidnapped, will you help bring him back?";
	}

	private string GetAcceptedText(){
		return "Thank you so much! Please bring him back safely"; 
	}
}
