using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "character", order = 0)]
public class Character: ScriptableObject
{
    public string char_name;
    public int health;
    public int[] breaks;
    public Sprite[] char_sprites = new Sprite[5];
    public CardAbility[] char_preCards = new CardAbility[7];

}
