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
    public Character[] characters;
    public StoryScript beforeStory;
    public StoryScript afterStory;
    public List<AddStage> priceStage;
    public CardAbility priceCard;
    public bool victoryed = false;
    public int avaliblePrice = 100;


    [Header("Advanced"), Space(15f)]
    public Stage playerStageLock;
    public List<Character> priceChars;
    public List<CardAbility> priceCards;
    public int tutorialLine;
    public bool noBreakCards;
    public bool noPrice;

    public int GetPriceSum(){
      int newint = 0;
      foreach(Character chars in characters){
         if(chars == null){break;}
         foreach(CardAbility card in chars.char_preCards){
            if(card == null){break;}
            newint += card.price;
         }
      }
      return newint;
    }


    public void ResetOnExitPlay()
     {
        victoryed = false;
        noPrice = false;

     } 
}
