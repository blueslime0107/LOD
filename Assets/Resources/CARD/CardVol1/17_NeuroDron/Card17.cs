using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card17 : CardAbility
{

    public override void OnDamage(CardPack card, Player attackerbm, Damage damage, BattleManager match)
    {
        
        if(damage.value <= 2){
            damage.value = 0;
            match.CardLog("Avoid",card);
            EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,0,true);
        }
    }
}
