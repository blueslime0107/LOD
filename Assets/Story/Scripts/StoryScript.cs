using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Story", order = 4)]
public class StoryScript : ScriptableObject
{
    public string scriptName;
    public string[] text_path;
    public List<CharObj> charStd;
    public float[] charPos;
}
