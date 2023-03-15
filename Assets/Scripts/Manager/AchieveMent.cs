using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveMent : ScriptableObject , IResetOnExitPlay
{
    public bool stack;
    public string quest_text;
    public int count;
    public int max_count;

    public bool achieved;

    public int questNum;

    [HideInInspector]public CardPack cardPack;
    [HideInInspector]public string tag;

    public virtual void OnCardActive(){}
    public virtual void OnBattleFoward(){}

    public bool Condi(int cardnum, string tagl){
      return cardPack.ability.card_id.Equals(cardnum) && tag.Equals(tagl);
    }

    public void isStack(){
      if(!stack){
         count = 0;
      }
    }

    public void addcount(){
        count++;
        if(count >= max_count){Finish();}
    }

    public void Finish(){
      achieved = true;
    }

    public void ResetOnExitPlay()
     {
        count = 0;
        achieved = false;

     } 
}
