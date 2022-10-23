using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "Stage", order = 0)]
public class Stage: ScriptableObject
{
    public string title;
    public string sub_text;
    public int rank;
    public string values;
    public Character[] characters = new Character[5];
    public StoryScript beforeStory;
    public StoryScript afterStory;
    public int price;
    public bool victoryed = false;
}
