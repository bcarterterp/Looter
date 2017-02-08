using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : CardLogic
{

    private enum MonsterType
    {
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
    private string roundRecap;

    public void EncounterRandomMonster(int level)
    {
        monsterType = (MonsterType)Random.Range(0, (int)MonsterType.TOTAL_MONSTER_TYPES);
        switch (monsterType)
        {
            case MonsterType.HUMAN_THUG:
                monster = new HumanThug(level);
                break;
            case MonsterType.BANDIT_GOBLIN:
                monster = new BanditGoblin(level);
                break;
            case MonsterType.CYCLOPS:
                monster = new Cyclops(level);
                break;
            case MonsterType.NECRO_MAGE:
                monster = new NecroMage(level);
                break;
            case MonsterType.PHANTOM:
                monster = new Phantom(level);
                break;
            case MonsterType.SKELETON:
                monster = new Skeleton(level);
                break;
            case MonsterType.ZOMBIE:
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

    private string GetFightRoundRecap()
    {
        return roundRecap;
    }

    private string GetMonsterDefeated()
    {
        switch (monsterType)
        {
            case MonsterType.HUMAN_THUG:
                return "Wait till I call my buddies!";
            case MonsterType.BANDIT_GOBLIN:
                return "I hope you blade find your heart!";
            case MonsterType.CYCLOPS:
                return "Can't stand no more";
            case MonsterType.NECRO_MAGE:
                return "I can never die!";
            case MonsterType.PHANTOM:
                return "Disappeared into a woosh of smoke";
            case MonsterType.SKELETON:
                return "The skeleton crumbles into a bed of bones";
            case MonsterType.ZOMBIE:
                return "Body falls limp";
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
        return GetStage() == 2;
    }
}
