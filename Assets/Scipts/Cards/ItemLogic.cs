using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : CardLogic {

    private Character monster;
    private Item item;
	private string roundRecap;

    public void GenerateItem(int level)
    {
		item = new Item(level);
        if (Random.Range(0, 1) == 0)
        {
            monster = new Phantom(level);
			monster.setHealth (monster.getHealth () + item.getHealth ());
			monster.setCurrHealth (monster.getHealth ());
			monster.setAttack (monster.getAttack () + item.getAttack ());
			monster.setCurrAttack (monster.getAttack ());
			monster.setDefence (monster.getDefence () + item.getDef ());
			monster.setCurrDef (monster.getCurrDef ());
        }
		ShouldProgress ();
    }

	public void InteractWithItem(Hero hero){
		switch (GetStage ()) {
		case 2:
			FightMonster (hero);
			break;
		case 3:
			AquireItem (hero);
			break;
		default:
			ShouldProgress ();
			break;
		}
	}

    public void FightMonster(Hero hero)
    {
		roundRecap = "";
		if (hero.getCurrAttack () <= monster.getCurrDef () && monster.getCurrAttack () <= hero.getCurrDef ()) {
			roundRecap = "You two are equal in power";
			hero.loseHealth (1);
			monster.loseHealth (1);
		} else {
			if (hero.CharacterWillMiss (monster)) {
				roundRecap += "You miss and";
			} else {
				int damage = hero.fightCharacter (monster);
				roundRecap += "You slash for " + damage + "damage and";
			}

			if (monster.CharacterWillMiss (hero)) {
				roundRecap += " the monster misses you";
			} else {
				int damage = monster.fightCharacter (hero);
				roundRecap += " they slash for " + damage + "damage";
			}
		}
		if (monster.getCurrHealth() <= 0)
		{
			roundRecap += " The spirit is exercised from the item";
			hero.gainExperience(monster.getExperience());
			hero.AdjustGold(monster.getGold());
			monster = null;
			ShouldProgress ();
		}
    }

	public void AquireItem(Hero hero){
		hero.AquireEquipment (item);
	}

    public override bool IsLastStage()
    {
        return GetStage() == 3;
    }

    public override Character GetMonster()
    {
        return monster;
    }

    public override Item GetItem()
    {
        return item;
    }

	public override string GetStoryText(){
		switch (GetStage())
		{
		case 0:
			return GetDiscoveryText();
		case 1:
			return GetApproachItemText();
		case 2:
			return GetFightRoundRecap();
		case 3:
			return GetAquireItemText();
		default:
			return "What!?!?!?";
		}
	}

	public string GetDiscoveryText(){
		return "You see a " + item.getType().ToString() +" lying on the ground";
	}

	public string GetApproachItemText(){
		if (monster != null) {
			return "You fool! Your greed will kill you!";
		} else {
			NextStage ();
			return "You are able to fully examine the " + item.getType().ToString();
		}
	}

	public string GetAquireItemText(){
		return "You will make good use of this item";
	}

	private string GetFightRoundRecap()
	{
		return roundRecap;
	}

}
