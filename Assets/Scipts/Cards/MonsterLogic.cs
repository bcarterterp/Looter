using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : CardLogic{

	private enum MonsterType{
		HUMAN_THUG,
		BANDIT_GOBLIN,
		CYCLOPS,
		NECRO_MAGE,
		PHANTOM,
		SKELETON,
		ZOMBIE,
		TOTAL_MONSTER_TYPES
	}

    private Character monster;
	private MonsterType monsterType;

    public void EncounterRandomMonster(int level)
    {
		monsterType = (MonsterType)Random.Range(0, (int)MonsterType.TOTAL_MONSTER_TYPES - 1);
		switch (monsterType) {
		case MonsterType.HUMAN_THUG:
			monster = new HumanThug (level);
			break;
		case MonsterType.BANDIT_GOBLIN:
			monster = new BanditGoblin (level);
			break;
		case MonsterType.CYCLOPS:
			monster = new Cyclops (level);
			break;
		case MonsterType.NECRO_MAGE:
			monster = new NecroMage (level);
			break;
		case MonsterType.PHANTOM:
			monster = new Phantom (level);
			break;
		case MonsterType.SKELETON:
			monster = new Skeleton (level);
			break;
		case MonsterType.ZOMBIE:
			monster = new Zombie (level);
			break;
		}
    }

    public void FightMonster(Hero hero)
    {
        if (hero.getCurrAttack() <= monster.getCurrDef() && monster.getCurrAttack() <= hero.getCurrDef())
        {
            hero.loseHealth(1);
            monster.loseHealth(1);
        }
        else
        {
            hero.fightCharacter(monster);
            monster.fightCharacter(hero);
        }

        if (monster.getCurrHealth() <= 0)
        {
            hero.gainExperience(monster.getExperience());
            hero.AdjustGold(monster.getGold());
            NextStage();
        }
    }

	public override string GetStoryText ()
	{
		switch (GetStage()) {
		case 0:
			return GetMonsterEncounteredText ();
		case 1:
			return "What!?!?!";
		default:
			return "What!?!?!";
		}
			
	}

	public string GetMonsterEncounteredText(){
		switch (monsterType) {
		case MonsterType.HUMAN_THUG:
			return "AY, youz wanna fight!!?!?";
		case MonsterType.BANDIT_GOBLIN:
			return "Time to slice and dice!";
		case MonsterType.CYCLOPS:
			return "Stay still, so I can hit you!";
		case MonsterType.NECRO_MAGE:
			return "I will add you to my collection";
		case MonsterType.PHANTOM:
			return "Diiiiiiiieeeeeeee";
		case MonsterType.SKELETON:
			return "Boned soldiers materialize behind you";
		case MonsterType.ZOMBIE:
			return "Braaains";
		default:
			return "What!?!?!";
		}
	}

    public Character GetMonster()
    {
        return monster;
    }

    public override bool IsLastStage()
    {
        return GetStage() == 1;
    }
}
