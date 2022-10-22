using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [Tooltip("캐릭터")]
    public string name;
    [Tooltip("대사")]
    public string[] context;
    [Tooltip("대사")]
    public int char_sprite;
    [Tooltip("대사")]
    public int char_condi;

    public int char_show;

}
