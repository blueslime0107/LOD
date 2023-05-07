using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card19 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        foreach(Player player in card.player.team.players){
            match.GiveCard(linked_card[0],player);
        }
        match.DestroyCard(card,card.player);
    }
}
