using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card46 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        foreach(Player player in card.player.team.players){
            if(player.Equals(card.player)){continue;}
            player.AddHealth((int)Mathf.Ceil(damage.value / 2));
        }
        match.backColorEff.changeColor(94, 94, 94,210);
    }

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player){return;}
        foreach(Player player in dead_player.team.players){
            if(player.Equals(card.player)){continue;}
            player.NewDamagedByInt(4,card.player);
        }
    }
}
