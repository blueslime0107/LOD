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
    public List<CardAbility> cards = new List<CardAbility>();
    public List<CardAbility> game_cards = new List<CardAbility>();

    public GameObject cardViewer;
    public GameObject blackScreen;

    public bool battle_ready;
    public bool battle_start;
    public bool battle_end = false;

    public int card_draw = 0;
    public bool card_gived = false;

    [HideInInspector]public int target1;
    [HideInInspector]public int target2;

    [HideInInspector]public bool battleing;

    [HideInInspector]public int cardViewChar_left;
    [HideInInspector]public int cardViewChar_right;

    public void Battle(){
        StartCoroutine("BattleMain");
    }

    // void Update(){

        
    // }

    IEnumerator BattleMain() {   
        while(true){ // 계속반복
            if(game_cards.Count<1)
                game_cards = CardSuffle();
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


            PlayerGoToOrigin();
            while(card_draw>0){
                if(game_cards.Count<1)
                    game_cards = CardSuffle();
                switch(game_cards.Count){
                    case 1:
                        Card(Vector3.up+Vector3.back,game_cards[0]);break;
                    case 2:
                        Card(Vector3.up+Vector3.right*2.5f+Vector3.back,game_cards[0]);
                        Card(Vector3.up+Vector3.left*2.5f+Vector3.back,game_cards[0]);break;
                    default:
                        Card(Vector3.up+Vector3.back,game_cards[0]);
                        Card(Vector3.up+Vector3.right*5+Vector3.back,game_cards[0]);
                        Card(Vector3.up+Vector3.left*5+Vector3.back,game_cards[0]);  break;
                }
                ui.cardMessage.SetActive(true);

                  
                           
                while(!card_gived){
                    yield return null;
                }
                card_gived = false;
            }
            
            
            BattlePreReset();
        }             
    }

    void PlayerGoToOrigin(){
        players[0].goto_origin = true;
        players[1].goto_origin = true;
        players[2].goto_origin = true;
        players[3].goto_origin = true;
        players[4].goto_origin = true;
        players[5].goto_origin = true;

    }

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
        for(int i = 0; i < players.Count; i++){
            for(int j = 0; j < players[i].cards.Count; j++){
                players[i].cards[j].MatchStarted(players[i],this);
            }
        }
        if(battle_ready){
            battle_start = true;
            battle_ready = false;
        }
    }

    void Card(Vector3 pos,CardAbility cardo){
        game_cards.Remove(cardo);
        GameObject card = Instantiate(cardViewer,pos,transform.rotation);
        CardDraw draw = card.GetComponent<CardDraw>();
        draw.battleManager = this;
        draw.ui = ui;
        draw.SetImage(cardo);
        
    }

    List<CardAbility> CardSuffle(){
        List<CardAbility> origin_cards = new List<CardAbility>(cards);
        List<CardAbility> suffle_cards = new List<CardAbility>();
        int origin_count = origin_cards.Count;
        for(int i = 0; i<origin_count;i++){
            int rand_card = Random.Range(0,origin_cards.Count);
            suffle_cards.Add(origin_cards[rand_card]);
            origin_cards.RemoveAt(rand_card);

        }
        return suffle_cards;

    }

}
