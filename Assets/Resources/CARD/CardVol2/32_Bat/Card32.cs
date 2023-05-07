using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/32샷건", order = 32)]
public class Card32 : CardAbility
{

    public override void OnClashDraw(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(battle.myChar != card.player){return;}
        battle.bm.CardLog("Draw",card);
        card.player.AddDice(3);
        
    }

}
