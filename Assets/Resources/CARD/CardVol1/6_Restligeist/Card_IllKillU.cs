using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6.5원한", order = 101)]
public class Card_IllKillU : CardAbility
{
    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        card.count += 2;
        player.DamagedBy(card.count,player);
    }
}