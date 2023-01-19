using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/32샷건", order = 32)]
public class Card32 : CardAbility
{

    public override void OnClashDraw(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.player == enemy){return;}
        card.player.AddDice(3);
        battle.bm.CardLog(card);
    }

}
