using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public BattleCaculate battleCaculate;
    public UI ui;
    public List<Dice> dices = new List<Dice>();
    public List<Dice_Indi> dice_indis = new List<Dice_Indi>();
    public List<Player> players = new List<Player>();

    public GameObject cardViewer;
    public GameObject blackScreen;

    public bool battle_ready;
    public bool battle_start;
    public bool battle_end = false;

    public int card_draw = 0;
    public bool card_gived = false;

    public int target1;
    public int target2;

    public bool battleing;

    public void Battle(){
        StartCoroutine("BattleMain");
    }

    void Update(){
        for(int i = 0;i <6;i++){
            ui.hpText[i].text = players[i].health.ToString();
        }

        
    }

    IEnumerator BattleMain() {   
        while(true){ // 계속반복
            yield return new WaitForSeconds(1f);
            DiceRoll(); // 주사위를 굴린다
            MakeADummy(true);
            while(true){ // 모든 캐릭터에게 주사위가 있으면 진행
                if(players[0].dice > 0 && players[1].dice > 0 && players[2].dice > 0 &&
                players[3].dice > 0 && players[4].dice > 0 && players[5].dice > 0){
                    battle_ready =  true;
                }
                if(battle_start)
                {
                    target1 = 0;
                    target2 = 0;
                    break;
                }
                    
                yield return null;
            }
            MakeADummy(false);
            while(!battle_end){ // 모든 캐릭터에게 주사위가 없으면 진행
                yield return null;
                if(battleing){
                    continue;
                }

                if(!battle_end){
                    if(players[0].dice <= 0 && players[1].dice  <= 0 && players[2].dice <= 0 &&
                    players[3].dice <= 0 && players[4].dice <= 0 && players[5].dice <= 0){
                        battle_end =  true;
                        break;
                    }
                }
                if(target1 > 0 && target2 > 0 && target1 != target2){
                    battleing = true;
                    blackScreen.SetActive(true);
                    battleCaculate.BattleMatch(target1,target2);
                }

                
            }
            while(card_draw>0){
                Card(Vector3.zero+Vector3.back,0);
                Card(Vector3.right*5+Vector3.back,1);
                Card(Vector3.left*5+Vector3.back,2);               
                while(!card_gived){
                    yield return null;
                }
                card_gived = false;
            }
            
            
            BattlePreReset();
        }             
    }

    // IEnumerator DrawCard() {
        
    //     Card(Vector3.zero+Vector3.back,0);
    //     Card(Vector3.right*5+Vector3.back,1);
    //     Card(Vector3.left*5+Vector3.back,2);


    //     yield return null;
    // }

    void DiceRoll(){
        for(int i = 0; i< dices.Count; i++)
            if(!players[i].died){
                dices[i].rolldice();
            }            
    }

    void MakeADummy(bool ver){
        if(ver == true){
            for(int i = 0; i< players.Count; i++)
                if(players[i].died){
                    players[i].SetDice(1);
                }     
        }      
        if(ver == false){
            for(int i = 0; i< players.Count; i++)
                if(players[i].died){
                    players[i].SetDice(0);
                }     
        }       
    }

    void BattlePreReset(){
        for(int i = 0; i< dices.Count; i++)
            dices[i].diceReroll();
        for(int i = 0; i< dices.Count; i++)
            dice_indis[i].isDiced = false;
        battle_start = false;
        battle_end = false;
        target1 = 0;
        target2 = 0;
    }

    public void BattleStart(){
        if(battle_ready){
            battle_start = true;
            battle_ready = false;
        }
    }

    void Card(Vector3 pos,int num){
        GameObject card = Instantiate(cardViewer,pos,transform.rotation);
        CardDraw draw = card.GetComponent<CardDraw>();
        draw.SetImage(num);
        draw.battleManager = this;
    }


}
