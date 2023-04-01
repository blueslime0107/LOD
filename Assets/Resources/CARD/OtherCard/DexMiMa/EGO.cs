using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGO : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.THEEGO = true;
        card.cardStyle = card.ability.overCard;
    }
}
