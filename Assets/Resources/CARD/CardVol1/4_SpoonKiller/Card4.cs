using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player == battle.myChar && card.player.dice > 0 && enemy.dice >= card.player.dice){
        card.player.SetDice(1);
        enemy.SetDice(0);
        battle.bm.CardLog("Damage",card,battle.eneChar);

        }

        

    }
}
