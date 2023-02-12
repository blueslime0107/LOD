using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Stage", order = 0)]
public class Stage: ScriptableObject, IResetOnExitPlay
{  
   public int id;
   public string[] xmlFile_path = new string[2];

    public string title;
    public string sub_text;
    public int rank;
    public string values;
    public Character[] characters = new Character[5];
    public StoryScript beforeStory;
    public StoryScript afterStory;
    public List<AddStage> priceStage;
    public bool victoryed = false;
    public int avaliblePrice = 100;
    public int charlimit = 5;


    [Header("Advanced"), Space(15f)]
    public Stage playerStageLock;
    public List<Character> priceChars;
    public List<CardAbility> priceCards;


    [Header("Special"), Space(15f)]
    public GameObject custom_stage;
    public string custom_BGM;
    public int tutorialLine;
    public bool noBreakCards;
    public bool noPrice;

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


    public void ResetOnExitPlay()
     {
        victoryed = false;
        noPrice = false;
        discovered = false;
     } 
}
