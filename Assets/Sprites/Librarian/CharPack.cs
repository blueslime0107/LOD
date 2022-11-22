using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Charpack", order = 2)]
public class CharPack : ScriptableObject
{
    public string name_;
    public Sprite[] poses = new Sprite[5];
    public bool farAtk = false;
}
