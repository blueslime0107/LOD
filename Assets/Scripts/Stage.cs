using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Stage", order = 0)]
public class Stage: ScriptableObject
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
}
