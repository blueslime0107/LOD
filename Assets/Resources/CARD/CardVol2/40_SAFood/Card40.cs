using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/9비상식량", order = 40)]
public class Card40 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card,BattleManager match)
    {
        card.player.max_health += 5;
        card.player.AddHealth(5);
    }

}