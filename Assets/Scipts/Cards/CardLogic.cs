using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic{

    private int stage;

    public CardLogic()
    {
        stage = 0;
    }

    public int GetStage()
    {
        return stage;
    }

    public void NextStage()
    {
        stage++;
    }

    public virtual bool IsLastStage()
    {
        return GetStage() == 0;
    }
}
