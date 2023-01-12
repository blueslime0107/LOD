using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/StoryCharacter", order = 5)]
public class CharObj : ScriptableObject
{
    public string name_;
    public List<Sprite> character = new List<Sprite>();
}
