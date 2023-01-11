using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/속도카드", order = 165)]
public class SpdCard : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        Dice copyDice = new Dice();
        for(int i=0;i<card.count;i++)
        {
            copyDice = new Dice();
            copyDice.dice_value = (int)Random.Range(0,6)+1;
            card.player.dice_Indi.put_subDice(copyDice);
            card.count -= 1;
        }
    }
    
    

    public override void StartMatch(CardPack card, BattleManager match){
        card.count = pre_count;
    }
    
}
