using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic{

    private int stage, progressToStage;
	private bool shouldProgress;

    public CardLogic()
    {
        stage = 0;
		progressToStage = -1;
		shouldProgress = false;
    }

	public virtual void Interact(Hero hero){

	}

	public virtual void Interact(Hero hero, int option){

	}

	public virtual void StageCheck(){
		if (shouldProgress) {
			if (progressToStage > -1) {
				stage = progressToStage;
			} else {
				NextStage ();
			}
			shouldProgress = false;
		}
	}

	public void ShouldProgress(){
		ShouldProgress (-1);
	}

	public void ShouldProgress(int requestedStage){
		shouldProgress = true;
		progressToStage = requestedStage;
	}

	public void ResetStage(){
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
        return GetStage() == 1;
    }

	public virtual string GetStoryText(){
		return "";
	}

    public virtual Item GetItem()
    {
        return null;
    }

    public virtual Item[] GetItems()
    {
        return new Item[0];
    }

    public virtual Character GetMonster()
    {
        return null;
    }

    public virtual int GetGold()
    {
        return -1;
    }
}
