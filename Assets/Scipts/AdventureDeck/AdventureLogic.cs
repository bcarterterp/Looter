
public class AdventureLogic
{

    private AdventureStack mainDeck;

    private Hero hero;
    private CardLogic cardLogic;
	private PotionLogic potionLogic;

    private Persister persister; 
    private int activeCard;

    public AdventureLogic()
    {
        persister = new Persister();
        this.hero = persister.LoadSavedHero();
        mainDeck = new AdventureStack();
		potionLogic = new PotionLogic ();
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
				MonsterLogic monsterLogic = new MonsterLogic();
				monsterLogic.EncounterRandomMonster(hero.getLevel());
                cardLogic = monsterLogic;
                break;
			case CardType.POTION:
				potionLogic.DiscoverPotion();
                cardLogic = potionLogic;
                break;
            case CardType.ITEM:
				ItemLogic itemLogic = new ItemLogic();
                itemLogic.GenerateItem(hero.getLevel());
				cardLogic = itemLogic;
                break;
            case CardType.OPAL:
				cardLogic = new OpalLogic();
                break;
			case CardType.MERCHANT:
				MerchantLogic merchantLogic = new MerchantLogic ();
				merchantLogic.GenerateItems(hero.getLevel());
                cardLogic = merchantLogic;
                break;
			case CardType.BLACKSMITH:
				BlacksmithLogic blacksmithLogic = new BlacksmithLogic();
                blacksmithLogic.SetReforgeableItems(hero);
				cardLogic = blacksmithLogic;
                break;
            case CardType.THIEF:
				cardLogic = new ThiefLogic();
                break;
            case CardType.WELL:
				cardLogic = new WellLogic();
                break;
			case CardType.GRAVE:
				cardLogic = new GraveLogic();
				break;
			case CardType.GAMBLER:
				cardLogic = new GamblerLogic ();
				break;
            case CardType.PASSERBY:
                cardLogic = new PasserbyLogic(mainDeck);
                break;
            case CardType.RESCUED:
                cardLogic = new RescuedLogic();
                break;
        }
		cardLogic.ResetStage();
        persister.SaveHero(hero);
        return activeCard;
    }

    public void InteractWithActiveCard()
    {
		cardLogic.Interact (hero);
        persister.SaveHero(hero);
    }

    public void InteractWithActiveCard(int choice)
    {
		cardLogic.Interact (hero, choice);
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
