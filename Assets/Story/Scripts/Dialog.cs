using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [Tooltip("캐릭터")]
    public string name;
    [Tooltip("스탠드")]
    public int char_feel;
    [Tooltip("위치")]
    public int char_pos;
    [Tooltip("대사")]
    public string context;
    
    

    public int char_show;

}
