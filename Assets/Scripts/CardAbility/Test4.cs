using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test4 : CardAbility
{
    public override void DiceApplyed(CardPack card, Player player)
    {
        player.SetDice(Mathf.Abs(player.dice-7));
    }

}

