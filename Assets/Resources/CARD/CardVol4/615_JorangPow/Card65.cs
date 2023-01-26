using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card65 : CardAbility
{
    public override void OnClashLose(CardPack card, BattleCaculate battle)
    {
        if(card.player.dice > 2){return;}
        battle.bm.CardLog("Avoid",card);
        battle.damage.setDamage(0);
        card.active = false;

    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.count ++;
        if(card.count >= 3){
            card.active = true;
        }
    }
}