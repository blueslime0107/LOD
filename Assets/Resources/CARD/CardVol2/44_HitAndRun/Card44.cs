using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/44치고빠지기", order = 44)]
public class Card44 : CardAbility
{

    public override void OnClashLose(CardPack card, BattleCaculate battle)
    {
        if(!battle.myChar.Equals(card.player)){return;}
        battle.damage.value = 0;
    }

}
