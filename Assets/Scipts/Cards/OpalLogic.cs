using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpalLogic : CardLogic
{

    Opal opal = new Opal();
    private int choice;

    public void RubOpal(Hero hero, int selection)
    {
        choice = selection;
        opal.RubOpal(choice, hero);
        ShouldProgress();
    }

    public override string GetStoryText()
    {
        switch (GetStage())
        {
            case 0:
                return GetDiscoverText();
            case 1:
                return GetShowOptionsText();
            case 2:
                return GetGoodByeText();
            default:
                return "What!?!?!?";
        }
    }

    public string GetDiscoverText()
    {
        ShouldProgress();
        return "I am an opal that can give you power!";
    }

    public string GetShowOptionsText()
    {
        ShouldProgress();
        return "Which blessing would you like?";
    }

    public string GetGoodByeText()
    {
        switch (choice)
        {
            case 0:
                return "You have been given health";
            case 1:
                return "You have been given defense";
            default:
                return "You have been given attack";
        }
    }

    public override bool IsLastStage()
    {
        return GetStage() == 2;
    }
}
