using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/19File_crashed", order = 19)]
public class Card19 : CardAbility
{

    public override void ImmediCardDraw(BattleManager match, Player player)
    {
        match.GiveCard(linked_card[0],player);
    }

    public override void StartMatch(BattleManager match)
    {
        base.StartMatch();
    }
}
