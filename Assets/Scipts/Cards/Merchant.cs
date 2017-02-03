using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant{

	public Item[] GetMerchantItems(int level)
    {
        Item[] items = new Item[3];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = new Item(level);
        }
        return items;
    }
}