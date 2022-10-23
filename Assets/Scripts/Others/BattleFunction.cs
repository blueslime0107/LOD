using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFunction : MonoBehaviour
{
    public BattleManager bm;

    public void DiceToPlayer(Dice dice, Player player){
        dice.gameObject.SetActive(false);

        player.dice_Indi.putDice(dice);
    }

    public void TargetPlayer(Player atk, Player def){
        if(!atk.gameObject.activeSelf){
            return;
        }
        if(!def.gameObject.activeSelf){
            return;
        }
        bm.target1 = atk;
        bm.target2 = def;
        bm.BattleTargetReady();
    }

    public string TeamBool2Str(){
        if(bm.left_turn){
            return "Left";
        }
        else if(bm.right_turn){
            return "Right";
        }
        Debug.Log("Error");
        return " ";
    }

    
}
