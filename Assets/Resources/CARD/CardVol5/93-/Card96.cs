using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card96 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-2);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        damage.adDamage(1);
    }
}
