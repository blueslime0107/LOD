using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/46소스피란도", order = 46)]
public class Card46 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        foreach(Player player in (attacker.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            if(player.Equals(card.player)){continue;}
            player.AddHealth((int)Mathf.Ceil(damage.value / 2));
        }
    }

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        foreach(Player player in (dead_player.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            if(player.Equals(card.player)){continue;}
            player.DamagedByInt(4,card.player);
        }
    }
}
