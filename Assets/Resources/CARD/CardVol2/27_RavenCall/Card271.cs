using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card271 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.team.carddraw += 1;
        card.player.cards.Remove(card);
    }
}
