using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoBattle : MonoBehaviour
{
    public BattleManager bm;

    public bool pressed;
    public TextMeshProUGUI text;

    public void Start(){
        text.text = "AUTODICE";
    }
    
    public void AutoDice(){
        if(pressed){
            for(int i=0;i<bm.left_team.players.Count;i++){
            if(!bm.left_team.players[i].dice_com.rolled){continue;}
            bm.left_team.players[i].dice_com.rolled = false;
            bm.left_team.players[i].dice_com.gameObject.SetActive(false);
            bm.left_team.players[i].dice_Indi.putDice(bm.left_team.players[i].dice_com);

            }    

            pressed = false;
            text.text = "AUTODICE";
            return;

        }
        for(int i=0;i<bm.left_team.players.Count;i++){
            bm.left_team.players[i].dice_com.transform.position = bm.left_team.players[i].dice_Indi.transform.position; 
        
        }
        text.text = "AUTOPUT";
        pressed = true;
    }
}
