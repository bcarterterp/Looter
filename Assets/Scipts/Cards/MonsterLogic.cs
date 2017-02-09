using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : CardLogic
{

    private Character monster;
    private MonsterTypes monsterType;
    private string roundRecap;

    public void EncounterRandomMonster(int level)
    {
        monsterType = (MonsterTypes)Random.Range(0, (int)MonsterTypes.TOTAL_MONSTER_TYPES);
        switch (monsterType)
        {
            case MonsterTypes.HUMAN_THUG:
                monster = new HumanThug(level);
                break;
            case MonsterTypes.BANDIT_GOBLIN:
                monster = new BanditGoblin(level);
                break;
            case MonsterTypes.CYCLOPS:
                monster = new Cyclops(level);
                break;
            case MonsterTypes.NECRO_MAGE:
                monster = new NecroMage(level);
                break;
            case MonsterTypes.PHANTOM:
                monster = new Phantom(level);
                break;
            case MonsterTypes.SKELETON:
                monster = new Skeleton(level);
                break;
            case MonsterTypes.ZOMBIE:
                monster = new Zombie(level);
                break;
        }
		ShouldProgress ();
    }

	public void InteractWithMonster(Hero hero){
		if (GetStage () == 1) {
			FightMonster (hero);
		}
	}

    public void FightMonster(Hero hero)
    {
        roundRecap = "";
        if (hero.getCurrAttack() <= monster.getCurrDef() && monster.getCurrAttack() <= hero.getCurrDef())
        {
            roundRecap = "You two are equal in power";
            hero.loseHealth(1);
            monster.loseHealth(1);
        }
        else
        {
            if (hero.CharacterWillMiss(monster))
            {
                roundRecap += "You miss and";
            }else
            {
                int damage = hero.fightCharacter(monster);
                roundRecap += "You slash for " + damage + "damage and";
            }

            if (monster.CharacterWillMiss(hero))
            {
                roundRecap += "the monster misses you";
            }
            else
            {
                int damage = monster.fightCharacter(hero);
                roundRecap += "they slash for " + damage + "damage";
            }
        }

        if (monster.getCurrHealth() <= 0)
        {
			roundRecap = GetMonsterDefeated ();
            hero.gainExperience(monster.getExperience());
            hero.AdjustGold(monster.getGold());
			NextStage();
        }
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetMonsterEncounteredText();
            case 1:
                return GetFightRoundRecap();
            case 2:
                return GetMonsterDefeated();
            default:
                return "What!?!?!?";
        }

    }

    private string GetMonsterEncounteredText()
    {
        switch (monsterType)
        {
            case MonsterTypes.HUMAN_THUG:
                return "AY, youz wanna fight!!?!?";
            case MonsterTypes.BANDIT_GOBLIN:
                return "Time to slice and dice!";
            case MonsterTypes.CYCLOPS:
                return "Stay still, so I can hit you!";
            case MonsterTypes.NECRO_MAGE:
                return "I will add you to my collection";
            case MonsterTypes.PHANTOM:
                return "Diiiiiiiieeeeeeee";
            case MonsterTypes.SKELETON:
                return "Boned soldiers materialize behind you";
            case MonsterTypes.ZOMBIE:
                return "Braaains";
            default:
                return "What!?!?!";
        }
    }

    private string GetFightRoundRecap()
    {
        return roundRecap;
    }

    private string GetMonsterDefeated()
    {
        switch (monsterType)
        {
            case MonsterTypes.HUMAN_THUG:
                return "Wait till I call my buddies!";
            case MonsterTypes.BANDIT_GOBLIN:
                return "I hope you blade find your heart!";
            case MonsterTypes.CYCLOPS:
                return "Can't stand no more";
            case MonsterTypes.NECRO_MAGE:
                return "I can never die!";
            case MonsterTypes.PHANTOM:
                return "Disappeared into a woosh of smoke";
            case MonsterTypes.SKELETON:
                return "The skeleton crumbles into a bed of bones";
            case MonsterTypes.ZOMBIE:
                return "Body falls limp";
            default:
                return "What!?!?!";
        }
    }

    public override Character GetMonster()
    {
        return monster;
    }

    public override bool IsLastStage()
    {
        return GetStage() == 2;
    }
}
