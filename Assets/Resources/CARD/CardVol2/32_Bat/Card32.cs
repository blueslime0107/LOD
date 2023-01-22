using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/32샷건", order = 32)]
public class Card32 : CardAbility
{

    public override void OnClashDraw(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.player == enemy){return;}
        battle.bm.CardLog(card);
        enemy.NewDamagedByInt(3,card.player);
        
    }

}
