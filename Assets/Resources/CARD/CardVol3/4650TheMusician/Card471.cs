using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card471 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.player.AddHealth(2);
        match.DestroyCard(card,card.player);
        card.player.ShowCardDeck(true,true);
    }
}
