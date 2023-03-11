using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/35페일노트", order = 35)]
public class Card35 : CardAbility
{

    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(battle.myChar != card.player){return;}
        battle.eneChar.AddDice(-2);
        EffectPlayerSet(card.effect[0],card.player,battle.eneChar.dice_Indi.transform,1,0);

        battle.bm.CardLog("PowerDown",card,battle.eneChar);
    }
}
