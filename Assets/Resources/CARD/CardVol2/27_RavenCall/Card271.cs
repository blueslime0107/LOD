using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card271 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.AddCardPoint(card.player.team);
        match.DestroyCard(card,card.player);
    }
}
