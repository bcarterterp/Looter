
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
	private GraveLogic graveLogic;
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
		graveLogic = new GraveLogic ();
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
				potionLogic.DiscoverPotion ();
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
            case CardType.THIEF:
                cardLogic = thiefLogic;
                break;
            case CardType.WELL:
                cardLogic = wellLogic;
                break;
			case CardType.GRAVE:
				cardLogic = graveLogic;
				break;
        }
		cardLogic.ResetStage();
        persister.SaveHero(hero);
        return activeCard;
    }

    public void InteractWithActiveCard()
    {
        switch ((CardType)activeCard)
        {
            case CardType.MONSTER:
                monsterLogic.InteractWithMonster(hero);
                break;
            case CardType.POTION:
                potionLogic.DrinkPotion(hero);
                break;
			case CardType.ITEM:
				itemLogic.InteractWithItem (hero);
                break;
            case CardType.THIEF:
                thiefLogic.Steal(hero);
                break;
            case CardType.WELL:
                wellLogic.RefreshHero(hero);
                break;
			case CardType.GRAVE:
				if (graveLogic.GetStage () == 3) {
					graveLogic.FightMonster (hero);
				}
				break;
        }
        persister.SaveHero(hero);
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
			case CardType.GRAVE:
				graveLogic.GraveOption (hero, choice);
				break;
        }
        persister.SaveHero(hero);
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

	public bool IsLastStage(){
		return cardLogic.IsLastStage();
	}

    public Character GetMonster()
    {
        return cardLogic.GetMonster();
    }

	public Item GetItem(){
        return cardLogic.GetItem();
	}

    public Item[] GetItems()
    {
        return cardLogic.GetItems();
    }

    public int GetGold()
    {
        return cardLogic.GetGold();
    }

	public string GetCardText(){
		return cardLogic.GetStoryText();
	}

    public bool AdventureComplete()
    {
        return mainDeck.CardCount() == 0;
    }

	public void StageCheck(){
		if (cardLogic != null) {
			cardLogic.StageCheck ();
		}
	}
}
