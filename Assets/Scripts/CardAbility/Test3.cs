using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Test3 : CardAbility
{



    public override void MatchStartedForDice(Dice_Indi dice, BattleManager match)
    {
        List<Dice_Indi> dice_list = match.all_dice;
        dice_list.Remove(dice);
        int count = 0;
        for(int i = 0; i<6; i++){
            foreach(Dice_Indi die in dice_list){
                if(die.value.Equals(dice.value+count)){
                    count++;
                    break;
                }
            }
        }
        dice.setDice(dice.value + count);

    }

}

