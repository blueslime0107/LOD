using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public List<CardAbility> ALLCARDS;
    public List<Stage> ALLSTAGES;

    public List<CardAbility> LoadCards(List<int> cardList){
        List<CardAbility> cards = new List<CardAbility>();
        foreach(int i in cardList){
            cards.Add(ALLCARDS[i]);
        }
        return cards;
    }

    public List<Stage> LoadStages(List<int> cardList){
        List<Stage> stages = new List<Stage>();
        foreach(int i in cardList){
            stages.Add(ALLSTAGES[i]);
        }
        return stages;
    }
}
