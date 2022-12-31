using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Character", order = 1)]
public class Character: ScriptableObject
{
    public int health;
    public List<int> breaks = new List<int>();
    public CharPack char_sprites;
    public CardAbility[] char_preCards = new CardAbility[7];
}
