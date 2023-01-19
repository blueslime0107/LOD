using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card201 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        foreach(CardAbility cards in card.ability.linked_card){
            match.GiveCard(cards,card.player);
        }
        match.DestroyCard(card,card.player);
        match.CardLog(card);
    }
}
