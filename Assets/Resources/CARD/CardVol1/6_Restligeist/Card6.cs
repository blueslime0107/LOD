using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card6 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player){return;}
        match.GiveCard(linked_card[0],dead_player.lastHit,true);
        match.CardLog("GiveCard",card,dead_player.lastHit);
    }
}