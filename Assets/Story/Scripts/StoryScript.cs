using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Story", order = 4)]
public class StoryScript : ScriptableObject
{
    public TextAsset[] texts;
    public List<CharObj> charStd;
}
