using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveLogic : CardLogic {

    private int option;
    private Item item;
    private int gold;
    private Character monster;
	private string roundRecap;

	public override void Interact (Hero hero)
	{
		if (GetStage () == 3) {
			FightMonster (hero);
		}
	}

	public override void Interact (Hero hero, int option)
	{
		GraveOption (hero, option);
	}

	public void GraveOption(Hero hero, int choice){
		gold = 0;
		option = choice;
		if (Random.Range (0, 2) == 0) {
			monster = new Zombie (hero.getLevel ());
			ShouldProgress ();
		} else {
			ShouldProgress (4);
		}
		if (Random.Range (0, 2) == 0) {
			item = new Item (hero.getLevel ());
		} else {
			item = null;
			gold = Random.Range (0, 41) + 10;
		}
	}

	public void FightMonster(Hero hero)
	{
		roundRecap = "";
		if (hero.getCurrAttack () <= monster.getCurrDef () && monster.getCurrAttack () <= hero.getCurrDef ()) {
			roundRecap = "You two are equal in power.";
			hero.loseHealth (1);
			monster.loseHealth (1);
		} else {
			if (hero.CharacterWillMiss (monster)) {
				roundRecap += "You miss, and ";
			} else {
				int damage = hero.fightCharacter (monster);
				roundRecap += "You slash for " + damage + "damage, and ";
			}

			if (monster.CharacterWillMiss (hero)) {
				roundRecap += " the monster misses you.";
			} else {
				int damage = monster.fightCharacter (hero);
				roundRecap += " they slash for " + damage + "damage.";
			}
		}
		if (monster.getCurrHealth() <= 0)
		{
			roundRecap += " And you find ";
			if (item != null) {
				roundRecap += "a "+item.GetType ().ToString ();
			} else {
				roundRecap += gold + " coins";
			}
			hero.gainExperience(monster.getExperience());
			monster = null;
			ShouldProgress ();
		}
	}

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoveryText();
			case 1:
				return GetApproachText ();
			case 2:
				return GetOptionSelectedText ();
			case 3:
				return GetFightRoundRecap();
			case 4:
				return GetAcceptGiftText ();
            default:
                return "What!?!?!";
        }
    }

    private string GetDiscoveryText()
    {
		ShouldProgress ();
        return "You see a figure on the ground in the distance.";
    }

	private string GetApproachText(){
		ShouldProgress ();
		return "You stumble upon a body that is half buried, shovel laying inches away";
	}

	private string GetOptionSelectedText(){
		switch (option) {
		case 0:
			if (monster != null) {
				return "You move to pick up the shovel, the body comes to life and attacks you!";
			} else {
				return "A boy shows up, and thanks you for your deed. He gives you a gift";
			}
		case 1:
			if (monster != null) {
				return "You start patting the body looking for valuables, it's eves open wide and attacks you!";
			} else {
				return "As you pat around looking for valuables, you find something!";
			}
		default:
			return "WHAT!?!?!?";
		}
	}

	private string GetAcceptGiftText(){
		return "You take it, and carry on your journey.";
	}

	private string GetFightRoundRecap()
	{
		return roundRecap;
	}

	public override Character GetMonster(){
		return monster;
	}

	public override Item GetItem ()
	{
		return item;
	}

	public override int GetGold ()
	{
		return gold;
	}

	public override bool IsLastStage ()
	{
		return GetStage () == 4;
	}
}
