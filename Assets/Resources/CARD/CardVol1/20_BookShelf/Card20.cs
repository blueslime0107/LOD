using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card20 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        
        if(!card.player.Equals(dead_player)){return;}
        match.CardLog(card);
        List<CardAbility> myCards = new List<CardAbility>();
        foreach(CardPack cards in card.player.cards){
            if(cards.Equals(card)){continue;}
            myCards.Add(cards.ability);
        }
        linked_card[0].linked_card = myCards;
        match.SpecialCardGet(card.player.team,linked_card);
        card.player.team.carddraw += 1;
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.CardLog(card);
        card.player.team.carddraw += 1;
    }
}
