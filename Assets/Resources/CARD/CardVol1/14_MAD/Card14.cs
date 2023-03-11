using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card14 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        if(damage.value <= 0){return;}
        Active(card);
        foreach(Player player in attacker.team.players){
            match.CardLog("Damage",card,player);
            player.DamagedByInt(1, card.player,damage,card);
            EffectPlayerSet(card.effect[attacker.team.players.IndexOf(player)],card.player,card.player.transform).movPoint = player.transform.position;
            
        }
        
    }
}
