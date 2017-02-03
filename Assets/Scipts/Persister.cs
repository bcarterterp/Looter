using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persister{

    public void SaveHero(Hero hero)
    {
        PlayerPrefs.SetInt("HERO_HEALTH_MAX", hero.getHealth());
        PlayerPrefs.SetInt("HERO_HEALTH_CURR", hero.getCurrHealth());
        PlayerPrefs.SetInt("HERO_ATK_MAX", hero.getAttack());
        PlayerPrefs.SetInt("HERO_ATK_CURR", hero.getCurrAttack());
        PlayerPrefs.SetInt("HERO_DEF_MAX", hero.getDefence());
        PlayerPrefs.SetInt("HERO_DEF_CURR", hero.getCurrDef());
        PlayerPrefs.SetInt("HERO_LEVEL", hero.getLevel());
        PlayerPrefs.SetInt("HERO_EXP", hero.getExperience());
        PlayerPrefs.SetInt("HERO_GOLD", hero.getGold());

        for (int i = 0; i < (int)Item.ItemType.TOTAL_SLOTS; i++)
        {
            string key = "HERO_EQUIP_" + i;
            if (hero.hasEquipment((Item.ItemType)i))
            {
                Item item = hero.GetItem((Item.ItemType)i);
                PlayerPrefs.SetInt(key + "_TYPE", (int)item.getType());
                PlayerPrefs.SetInt(key + "_HEALTH", item.getHealth());
                PlayerPrefs.SetInt(key + "_ATK", item.getAttack());
                PlayerPrefs.SetInt(key + "_DEF", item.getDef());
                PlayerPrefs.SetInt(key + "_PRICE", item.getPrice());
                PlayerPrefs.SetInt(key, 1);
            }
            else
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.DeleteKey(key + "_TYPE");
                PlayerPrefs.DeleteKey(key + "_HEALTH");
                PlayerPrefs.DeleteKey(key + "_ATK");
                PlayerPrefs.DeleteKey(key + "_DEF");
                PlayerPrefs.DeleteKey(key + "_PRICE");
            }
        }
    }

    public Hero LoadSavedHero()
    {
        Hero hero = new Hero();
        if (PlayerPrefs.GetInt("HERO_LEVEL") != 0)
        {
            hero.setHealth(PlayerPrefs.GetInt("HERO_HEALTH_MAX"));
            hero.setCurrHealth(PlayerPrefs.GetInt("HERO_HEALTH_CURR"));
            hero.setAttack(PlayerPrefs.GetInt("HERO_ATK_MAX"));
            hero.setCurrAttack(PlayerPrefs.GetInt("HERO_ATK_CURR"));
            hero.setDefence(PlayerPrefs.GetInt("HERO_DEF_MAX"));
            hero.setCurrDef(PlayerPrefs.GetInt("HERO_DEF_CURR"));
            hero.setLevel(PlayerPrefs.GetInt("HERO_LEVEL"));
            hero.setExperience(PlayerPrefs.GetInt("HERO_EXP"));
            hero.setGold(PlayerPrefs.GetInt("HERO_GOLD"));

            for (int i = 0; i < (int)Item.ItemType.TOTAL_SLOTS; i++)
            {
                string key = "HERO_EQUIP_" + i;
                if (PlayerPrefs.GetInt(key) == 1)
                {
                    Item item = new Item();
                    item.setType((Item.ItemType)PlayerPrefs.GetInt(key + "_TYPE"));
                    item.setHealth(PlayerPrefs.GetInt(key + "_HEALTH"));
                    item.setAttack(PlayerPrefs.GetInt(key + "_ATK"));
                    item.setDef(PlayerPrefs.GetInt(key + "_DEF"));
                    item.setPrice(PlayerPrefs.GetInt(key + "_PRICE"));
                    hero.AddEquipment(item);
                }
            }
        }
        else
        {
            SaveHero(hero);
        }
        return hero;
    }

    public void SaveCardLibrary(CardLibray library)
    {
        string key = "CARD_";
        for(int i = 0; i < library.LibraryCount(); i++)
        {
            PlayerPrefs.SetInt(key + i, library.GetCardLevel(i));
        }
    }

    public void LoadCardLibrary(CardLibray library)
    {
        string key = "CARD_";
        for (int i = 0; i < library.LibraryCount(); i++)
        {
            int level = PlayerPrefs.GetInt(key + i, 0);
            library.SetCardLevel(i, level);
        }

        //Can be moved to a game init file
        if(library.GetCardLevel((int)CardType.MONSTER) == 0)
        {
            library.SetCardLevel((int)CardType.MONSTER, 1);
        }
        if (library.GetCardLevel((int)CardType.ITEM) == 0)
        {
            library.SetCardLevel((int)CardType.ITEM, 1);
        }
    }

    public void ClearGame()
    {
        PlayerPrefs.DeleteAll();
    }
}