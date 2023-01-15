using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card27 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player cardGetPlayer, CardPack getCard)
    {
        if(cardGetPlayer.team != card.player.team){return;}
        List<CardAbility> saved_cards = new List<CardAbility>();
        foreach(CardAbility cards in match.cur_game_cards){
            if(cards.Equals(linked_card[0])){return;}
            if(cards.Equals(getCard.ability)){continue;}
            saved_cards.Add(cards);
        }
        saved_cards.Add(linked_card[0]);
        match.SpecialCardGet(card.player.team,saved_cards);
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.team.carddraw += 1;
    }
}
