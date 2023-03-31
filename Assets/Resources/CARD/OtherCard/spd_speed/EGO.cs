using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/자아", order = 165)]

public class EGO : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.THEEGO = true;
        card.cardStyle = card.ability.overCard;
    }
}
