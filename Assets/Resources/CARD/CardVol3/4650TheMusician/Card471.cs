using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card471 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.player.AddDice(2);
        card.player.cards.Remove(card);
        card.player.ShowCardDeck(true,true);
    }
}
