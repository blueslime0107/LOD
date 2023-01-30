using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player.dice > enemy.dice && battle.myChar != card.player){return;}
        if(card.player.dice <= 0){return;}

        card.player.SetDice(1);
        enemy.SetDice(0);
        battle.bm.CardLog("Damage",card,battle.eneChar);
        

    }
}
