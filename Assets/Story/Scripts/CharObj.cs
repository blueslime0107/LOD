using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "charObj", order = 1)]
public class CharObj : ScriptableObject
{
    public List<Sprite> character = new List<Sprite>();
}
