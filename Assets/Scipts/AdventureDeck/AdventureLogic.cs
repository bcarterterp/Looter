
using UnityEngine;

public class AdventureLogic
{

    private AdventureStack mainDeck;

    private Hero hero;
    private MonsterLogic monsterLogic;
    private PotionLogic potionLogic;
    private ItemLogic itemLogic;
    private OpalLogic opalLogic;
    private MerchantLogic merchantLogic;
    private BlacksmithLogic blacksmithLogic;
    private ThiefLogic thiefLogic;
    private WellLogic wellLogic;
    private CardLogic cardLogic;

    private Persister persister; 
    private int activeCard;

    public AdventureLogic()
    {
        persister = new Persister();
        this.hero = persister.LoadSavedHero();
        mainDeck = new AdventureStack();
        monsterLogic = new MonsterLogic();
        potionLogic = new PotionLogic();
        itemLogic = new ItemLogic();
        opalLogic = new OpalLogic();
        merchantLogic = new MerchantLogic();
        blacksmithLogic = new BlacksmithLogic();
        thiefLogic = new ThiefLogic();
        wellLogic = new WellLogic();
    }

    public void StartGame()
    {
        mainDeck.CreateDungeonDeck();
        activeCard = -1;
    }

    public void Restart()
    {
        mainDeck.PopAll();
        StartGame();
    }

    public int DrawCard()
    {
        activeCard = mainDeck.Pop();
        switch ((CardType)activeCard)
        {
            case CardType.MONSTER:
                cardLogic = monsterLogic;
                monsterLogic.EncounterRandomMonster(hero.getLevel());
                break;
            case CardType.POTION:
                cardLogic = potionLogic;
                break;
            case CardType.ITEM:
                cardLogic = itemLogic;
                itemLogic.GenerateItem(hero.getLevel());
                break;
            case CardType.OPAL:
                cardLogic = opalLogic;
                break;
            case CardType.MERCHANT:
                merchantLogic.GenerateItems(hero.getLevel());
                cardLogic = merchantLogic;
                break;
            case CardType.BLACKSMITH:
                cardLogic = blacksmithLogic;
                blacksmithLogic.SetReforgeableItems(hero);
                break;
            case CardType.THEIF:
                cardLogic = thiefLogic;
                break;
            case CardType.WELL:
                cardLogic = wellLogic;
                break;
        }
        persister.SaveHero(hero);
        return activeCard;
    }

    public void InteractWithActiveCard()
    {
        switch ((CardType)activeCard)
        {
            case CardType.MONSTER:
                monsterLogic.FightMonster(hero);
                break;
            case CardType.POTION:
                potionLogic.DrinkPotion(hero);
                break;
            case CardType.ITEM:
                if(itemLogic.GetMonster() == null)
                { 
                    hero.AquireEquipment(itemLogic.GetItem());
                }
                else
                {
                    itemLogic.FightMonster(hero);
                }
                break;
            case CardType.THEIF:
                thiefLogic.StealItem(hero);
                break;
            case CardType.WELL:
                wellLogic.RefreshHero(hero);
                break;
        }
        persister.SaveHero(hero);
        if (cardLogic.IsLastStage())
        {
            activeCard = -1;
			cardLogic.ResetStage();
        }
    }

    public void InteractWithActiveCard(int choice)
    {
        switch ((CardType)activeCard)
        {
            case CardType.OPAL:
                opalLogic.RubOpal(hero, choice);
                break;
            case CardType.MERCHANT:
                merchantLogic.BuyItem(hero, choice);
                break;
            case CardType.BLACKSMITH:
                blacksmithLogic.ReforgeItem(hero, choice);
                break;
        }
        persister.SaveHero(hero);
        if (cardLogic.IsLastStage())
        {
            activeCard = -1;
			cardLogic.ResetStage();
        }
    }

    public void Decline()
    {
        activeCard = -1;
    }

    public void WipeAllData()
    {
        Restart();
        persister.ClearGame();
    }

    public Hero GetHero()
    {
        return hero;
    }

    public int GetActiveCard()
    {
        return activeCard;
    }

	public int GetLogicStage(){
		return cardLogic.GetStage();
	}

    public Character GetMonster()
    {
        return monsterLogic.GetMonster();
    }

	public Item GetItem(){
		return null;
	}

    public Item[] GetItems()
    {
        switch ((CardType)activeCard)
        {
            case CardType.MERCHANT:
                return merchantLogic.GetItems();
            case CardType.BLACKSMITH:
                return blacksmithLogic.GetItems();
            default:
                return new Item[0];
        }
    }

}
