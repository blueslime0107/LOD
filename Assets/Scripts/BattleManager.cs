using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public UI ui;
    public List<Dice> dices = new List<Dice>();
    public List<Dice_Indi> dice_char = new List<Dice_Indi>();

    public bool battle_ready;
    public bool battle_start;

    public void Battle(){
        StartCoroutine("BattleMain");
    }

    IEnumerator BattleMain() {   
        while(true){
            yield return new WaitForSeconds(1f);
            DiceRoll();
            while(true){
                DiceNumberCheck();
                if(battle_start)
                    break;
                yield return null;
            }
            while(true){
                DiceNumberCheck();
                if(battle_start)
                    break;
                yield return null;
            }
            
        }             
    }

    void DiceRoll(){
        for(int i = 0; i< dices.Count; i++)
            dices[i].rolldice();
    }

    void DiceNumberCheck(){
        if(!battle_ready){
            if(dice_char[0].dice_value > 0 && dice_char[1].dice_value > 0 && dice_char[2].dice_value > 0 &&
            dice_char[3].dice_value > 0 && dice_char[4].dice_value > 0 && dice_char[5].dice_value > 0){
                battle_ready =  true;
            }
            else{
                battle_ready = false;
            }
        }
        
    }

    void BattleEndCheck(){
        if(!battle_ready){
            if(dice_char[0].dice_value > 0 && dice_char[1].dice_value > 0 && dice_char[2].dice_value > 0 &&
            dice_char[3].dice_value > 0 && dice_char[4].dice_value > 0 && dice_char[5].dice_value > 0){
                battle_ready =  true;
            }
            else{
                battle_ready = false;
            }
        }
        
    }

    public void BattleStart(){
        if(battle_ready){
            battle_start = true;
            battle_ready = false;
        }
        
    }
}
