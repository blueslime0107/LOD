using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/44치고빠지기", order = 44)]
public class Card44 : CardAbility
{

    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(!battle.myChar.Equals(card.player)){return;}
        battle.damage.value = 0;
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,1,0);
        battle.bm.CardLog("NoDamage",card);
    }

}
