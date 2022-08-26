using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test4 : CardAbility
{
    public override void DiceApplyed(Player player)
    {
        player.SetDice(Mathf.Abs(dice.value-7));
    }

}

