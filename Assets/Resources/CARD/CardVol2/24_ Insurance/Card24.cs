using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/24보험", order = 24)]
public class Card24 : CardAbility
{

    public override void WhenCardGet(CardPack card, BattleManager match, Player player)
    {
        Debug.Log("보험");
        if(!card.player.tag.Equals(player.tag)){return;}
        player.AddHealth(4);
    }
}
