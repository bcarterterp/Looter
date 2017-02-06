using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureStoryGenerator{

    public string GetAdventureTransitionText()
    {
        return "You leave the town looking for adventure";
    }

    public string GetCardDiscoveryText(int card)
    {
        return "Something happens related to this card";
    }

    public string GetInteractionText(int card)
    {
        return "This card did something";
    }

    public string GetCompletionText(int card)
    {
        return "You leave feeling stronger";
    }

    public string GetArrivalText()
    {
        return "You arrived at your destination";
    }

    public string GetAvoidText(int card)
    {
        return "You avoided this card";
    }

}
