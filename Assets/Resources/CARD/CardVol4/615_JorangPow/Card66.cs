using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card66 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.CardLog("Health",card);
        card.player.AddHealth(3);
        card.active = true;
    }

}