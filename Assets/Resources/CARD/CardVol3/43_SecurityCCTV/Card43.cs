using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/43경비용CCTV", order = 43)]
public class Card43 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard)
    {
        player.AddHealth(-1);
        match.CardLog(card,player);
    }
}
