using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdCard : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        Dice copyDice = new Dice();
        for(int i=0;i<pre_count;i++)
        {
            copyDice = new Dice();
            copyDice.dice_value = (int)Random.Range(0,6)+1;
            card.player.dice_Indi.put_subDice(copyDice);
        }
    }

}
