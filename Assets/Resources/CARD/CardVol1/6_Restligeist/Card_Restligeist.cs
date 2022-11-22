using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6지박령", order = 6)]
public class Card_Restligeist : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player){return;}
        match.GiveCard(linked_card[0],dead_player.lastHit);
    }
}