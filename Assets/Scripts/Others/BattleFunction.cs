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
        return bm.cur_team.text;
    }

    
}
