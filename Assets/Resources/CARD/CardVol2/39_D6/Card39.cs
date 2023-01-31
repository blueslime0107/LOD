using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/10D6", order = 39)]
public class Card39 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.AddCardPoint(card.player.team);
        card.player.team.diceRollGague += 3;
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.count = card.player.team.diceRollGague;
    }

}
