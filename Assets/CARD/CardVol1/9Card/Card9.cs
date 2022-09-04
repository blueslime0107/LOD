using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/9비상식량", order = 9)]
public class Card9 : CardAbility
{
    public override void ImmediCardDraw(BattleManager match, Player player)
    {
        player.max_health += 5;
        player.AddHealth(5);
    }

}
