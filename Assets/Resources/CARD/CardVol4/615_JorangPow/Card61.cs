using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card61 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog("Active",card);
        if(enemy.dice >= 6 && card.player.dice <= 2){
            battle.bm.CardLog("JoRang",card);
            card.player.AddDice(6);

        }
    }
}
