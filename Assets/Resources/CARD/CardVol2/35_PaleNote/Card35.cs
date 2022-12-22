using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/35페일노트", order = 35)]
public class Card35 : CardAbility
{

    public override void OnClashStart(CardPack card, BattleCaculate battle)
    {
        battle.eneChar.AddDice(-2);
    }
}
