using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Stage", order = 0)]
public class Stage: ScriptableObject, IResetOnExitPlay
{
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




    [Header("Advanced"), Space(15f)]
    public Stage playerStageLock;
    public List<Character> priceChars;
    public int tutorialLine;
    public bool noBreakCards;
    public bool noPrice;


    public void ResetOnExitPlay()
     {
        victoryed = false;
        noPrice = false;
     } 
}
