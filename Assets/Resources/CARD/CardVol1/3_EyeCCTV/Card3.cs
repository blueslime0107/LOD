using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3 : CardAbility
{

    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match,Player attacker,Player defender)
    {
        if(damage.value >= 3){
            card.player.AddHealth(2);
            match.CardLog("Health",card);
            EffectPlayerSet(card.effect[0],card.player,defender.transform,0,-2);
        }
    }
}
