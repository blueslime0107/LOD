using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Character", order = 1)]
public class Character: ScriptableObject
{
    public int health;
    public List<int> breaks = new List<int>();
    public CharPack char_sprites;
    public List<CardAbility> char_preCards;
    [HideInInspector]public bool battleAble = true;

    [Space(10f)]
    public GameObject specialAtk;
    public int priceBonus;
}
