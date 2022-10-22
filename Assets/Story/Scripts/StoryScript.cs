using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "storyScript", order = 0)]
public class StoryScript : ScriptableObject
{
    public string scriptName;
    [TextArea] public string sub_note;
    public CharObj[] char_list;
    public float[] char_x;
}
