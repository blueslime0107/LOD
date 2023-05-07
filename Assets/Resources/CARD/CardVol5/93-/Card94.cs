using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card94 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.player.AddDice(-Random.Range(0,card.count+1));
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.count = 1;
    }
}
