using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public UI ui;
    public List<Dice> dices = new List<Dice>();
    public List<Dice_Indi> dice_indis = new List<Dice_Indi>();
    public List<Player> players = new List<Player>();

    public bool battle_ready;
    public bool battle_start;
    public bool battle_end = false;

    public void Battle(){
        StartCoroutine("BattleMain");
    }

    void Update(){
        for(int i = 0;i <6;i++){
            ui.hpText[i].text = players[i].health.ToString();
        }
        
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
            while(!battle_start){
                DiceNumberCheck();
                yield return null;
            }
            while(!battle_end){
                BattleEndCheck();
                if(battle_end)
                    break;
                yield return null;
            }
            //yield return new WaitForSeconds(1f);
            for(int i = 0; i< dices.Count; i++)
                dices[i].diceReroll();
            for(int i = 0; i< dices.Count; i++)
                dice_indis[i].isDiced = false;
            battle_start = false;
            battle_end = false;
            
        }             
    }

    void DiceRoll(){
        for(int i = 0; i< dices.Count; i++)
            dices[i].rolldice();
    }

    void DiceNumberCheck(){
        if(players[0].dice > 0 && players[1].dice > 0 && players[2].dice > 0 &&
        players[3].dice > 0 && players[4].dice > 0 && players[5].dice > 0){
            battle_ready =  true;
        }
        
        
    }

    void BattleEndCheck(){
        if(!battle_end){
            if(players[0].dice <= 0 && players[1].dice  <= 0 && players[2].dice <= 0 &&
            players[3].dice <= 0 && players[4].dice <= 0 && players[5].dice <= 0){
                battle_end =  true;
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
