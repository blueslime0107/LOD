using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card50 : CardAbility
{

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.GiveCard(linked_card[0],card.player);
        foreach(Player players in match.players){
            if(players.died){
                match.GiveCard(linked_card[0],card.player);
            }
        }
    }

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        match.GiveCard(linked_card[0],card.player);
        match.CardLog("Add Flute",card);
    }
}
