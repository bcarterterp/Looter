using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantLogic : CardLogic {

    Merchant merchant;
    Item[] items;

    public MerchantLogic()
    {
        merchant = new Merchant();
    }

    public void GenerateItems(int level)
    {
        items = merchant.GetMerchantItems(level);
        NextStage();
    }

    public void BuyItem(Hero hero, int choice)
    {
        hero.AdjustGold(-items[choice].getPrice());
        hero.AquireEquipment(items[choice]);
        items = null;
        NextStage();
    }

    public override bool IsLastStage()
    {
        return GetStage() == 2;
    }

    public Item[] GetItems()
    {
        return items;
    }
}
