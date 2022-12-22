using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14상호확증파괴", order = 14)]
public class Card14 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        foreach(Player player in (attacker.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            player.DamagedByInt(1, card.player);
        }
    }
}
