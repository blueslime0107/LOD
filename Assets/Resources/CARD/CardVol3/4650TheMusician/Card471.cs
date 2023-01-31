using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card471 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.player.AddHealth(2);
        match.CardLog("Health",card);
        match.DestroyCard(card,card.player);
        card.player.ShowCardDeck(true,true);
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(card.player.health <= card.player.health/2-2){
            CardActivate(card,match);
        }
    }
}
