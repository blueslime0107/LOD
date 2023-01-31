using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card79 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.player.health - damage.value <= card.player.breakCount[0]){
            match.CardLog("My Card!",card,attacker);
            match.AddCardPoint(attacker.team);
            match.AddCardPoint(card.player.team, -1);
        }
    }
}
