using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flow : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.godMode = true;
        card.player.THEEGO = true;
        card.cardStyle = card.ability.overCard;
    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        card.player.godMode = true;
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        card.player.godMode = false;
    }
}
