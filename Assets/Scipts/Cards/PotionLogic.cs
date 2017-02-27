using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PotionLogic : CardLogic {

	private enum PotionType
	{
		WHITE,
		RED,
		GREEN,
		YELLOW,
		BROWN,
		PURPLE,
		BLUE,
		ORANGE
	}

    private Potion potion;
	private Dictionary<int, PotionType> discoveredPotions;
	private int effectNumber;
	private PotionType type;

	public PotionLogic(){
		discoveredPotions = new Dictionary<int, PotionType>();
		potion = new Potion();
	}

	public void DiscoverPotion(){
		potion.DiscoverPotion();
		effectNumber = Random.Range(0,Potion.TOTAL_OPTIONS);
		ShouldProgress ();
	}

	public override void Interact (Hero hero)
	{
		DrinkPotion (hero);
	}

	public void DrinkPotion(Hero hero)
    {
        potion.drinkPotion(hero);
		if(!discoveredPotions.ContainsKey(effectNumber)){
			discoveredPotions.Add(effectNumber, type);
		}
		ShouldProgress ();
    }

	public override string GetStoryText(){
		if (GetStage () == 0) {
			if (discoveredPotions.ContainsKey(effectNumber)) {
				return GetKnownPotionText();
			} else {
				return GetUnknownPotionText();	
			}
		} else {
			return GetDrinkPotionText();
		}
	}

	private List<PotionType> GetUnknownPotionTypes(){
		List<PotionType> values = Enumerable.ToList(discoveredPotions.Values);
		List<PotionType> unknownTypes = new List<PotionType> ();
		for(int i = 0;i<Potion.TOTAL_OPTIONS;i++){
			unknownTypes.Add((PotionType)i);
		}
		foreach(PotionType type in values){
			unknownTypes.Remove(type);
		}
		return unknownTypes;
	}

	private List<int> GetUnknownPotionEffects(){
		List<int> keys = Enumerable.ToList(discoveredPotions.Keys);
		List<int> unknownTypes = new List<int> ();
		for(int i = 0;i<Potion.TOTAL_OPTIONS;i++){
			unknownTypes.Add(i);
		}
		foreach(int index in keys){
			unknownTypes.Remove(index);
		}
		return unknownTypes;
	}

	private string GetUnknownPotionText(){

		List<PotionType> unknownTypes = GetUnknownPotionTypes();
		type = unknownTypes[Random.Range(0,unknownTypes.Count())];
		switch ((PotionType)type) {
		case PotionType.WHITE:
			return "Ghost White Potion";
		case PotionType.RED:
			return "Blood Red Potion";
		case PotionType.GREEN:
			return "Emerald Green Potion";
		case PotionType.YELLOW:
			return "Canary Yellow Potion";
		case PotionType.BROWN:
			return "Mud Brown Potion";
		case PotionType.PURPLE:
			return "Royal Purple Potion";
		case PotionType.BLUE:
			return "Sky Blue Potion";
		case PotionType.ORANGE:
			return "Orange Potion";
		default:
			return "What?!?!?";
		}
	}

	private string GetKnownPotionText(){
		switch (effectNumber) {
		case 0:
			return "Gain Health Potion";
		case 1:
			return "Lose Health Potion";
		case 2:
			return "Gain Attack Potion";
		case 3:
			return "Lose Attack Potion";
		case 4:
			return "Gain Defence Potion";
		case 5:
			return "Lose Defence Potion";
		case 6:
			return "Threatening Potion";
		case 7:
			return "Refreshment Potion";
		default:
			return "What?!?!?";
		}
	}

	private string GetDrinkPotionText(){
		switch (effectNumber) {
		case 0:
			return "You feel healthier";
		case 1:
			return "You lose lifeforce";
		case 2:
			return "You feel more powerful";
		case 3:
			return "You feel weaker";
		case 4:
			return "You feel tougher";
		case 5:
			return "You feel flimsy";
		case 6:
			return "You feel as though you can fight stronger foe!";
		case 7:
			return "You are refreshed";
		default:
			return "What?!?!?";
		}
	}

}
