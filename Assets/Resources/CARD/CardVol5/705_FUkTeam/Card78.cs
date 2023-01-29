using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card78 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count += 1;
        if(card.count < 2){return;}
        foreach(Player playe in card.player.team.players){
            if(playe.GetCharName() == "MaBubsa"){
                match.CardLog("Potato",card);
                match.GiveCard(linked_card[0],playe);
                card.count = 0;
                break;
            }
            
        }
    }
}
