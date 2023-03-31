using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Stage", order = 0)]
public class Stage: ScriptableObject, IResetOnExitPlay
{  
   public int id;
   public StageAddress stageAddress;

    [HideInInspector]public string title;

    [Space(10),Header("----------Stage----------")]
    public int rank;
    public Character[] characters = new Character[5];

    [Space(10),Header("----------Price----------")]
    public List<Stage> priceStage;
    public StagePlayerSave priceChars;
    public List<CardAbility> priceCards;

    [Space(10),Header("----------Story----------")]
    public StoryScript beforeStory;
    public StoryScript afterStory;

    
    [HideInInspector]public bool victoryed = false;
    [Space(10),Header("----------Limit----------")]
    public int charlimit = 5;


    [Space(15f),Header("----------Special----------")]
    public Stage playerStageLock;
    public GameObject custom_stage;
    public string custom_BGM;
   public bool noEditChar;

    public int tutorialLine;
    public bool noBreakCards;
    public bool noPrice;
    public bool noCardEquipBreak;

    [HideInInspector]public bool discovered;

    public int GetPriceSum(){
      int newint = 0;
      foreach(Character chars in characters){
         if(chars == null){break;}
         if(!chars.battleAble){continue;}
         foreach(CardAbility card in chars.char_preCards){
            if(card == null){break;}
            newint += card.price;
         }
      }
      return newint;
    }

    public int GetSelfPriceSum(Character player){
      int newint = 0;
         foreach(CardAbility card in player.char_preCards){
            if(card == null){break;}
            newint += card.price;
         }
      
      return newint;
    }

    public void AddCharacter(Character character){
      for(int i =0;i<characters.Length;i++){
         if(characters[i] == null){
            characters[i] = character;
            return;
         }
      }
    }


    public void ResetOnExitPlay()
     {
        victoryed = false;
        noPrice = false;
        discovered = false;
     } 
}
