using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveLogic : CardLogic {

    private int option;
    private Item takenItem;
    private int gold;
    private Character character;

    public void BuryBody()
    {
        
    }

    public void LootBody()
    {
        
    }

    public void LeaveBody()
    {

    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoveryText();
            default:
                return "What!?!?!";
        }
    }

    public string GetDiscoveryText()
    {
        return "You stumble upon a body that is half buried";
    }

}
