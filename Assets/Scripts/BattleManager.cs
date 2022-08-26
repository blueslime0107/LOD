using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public BattleCaculate battleCaculate;
    public new CameraCtrl camera;
    public BackGround backGround;
    public UI ui;
    public List<Dice> dices = new List<Dice>();
    
    public List<Player> players = new List<Player>();
    public List<CardAbility> cards = new List<CardAbility>();
    public List<CardAbility> game_cards = new List<CardAbility>();

    public List<Dice_Indi> left_dice = new List<Dice_Indi>();
    public List<Dice_Indi> right_dice = new List<Dice_Indi>();

    public GameObject cardViewer;
    public GameObject blackScreen;

    public bool battle_ready;
    public bool battle_start;
    public bool battle_end = false;

    public bool card_getting_team;
    public int card_left_draw = 0;
    public int card_right_draw = 0;
    public bool card_gived = false;

    public Dice_Indi target1;
    public Dice_Indi target2;

    [HideInInspector]public bool battleing;

    [HideInInspector]public int cardViewChar_left;
    [HideInInspector]public int cardViewChar_right;

    public bool first_turn;
    public bool right_turn;
    public bool left_turn;

    float gague_time;
    public float left_gague_max;
    public float right_gague_max;
    [SerializeField]float left_gague;
    [SerializeField]float right_gague;

    public float diceSort_scale;

    public void Battle(){
        left_gague = left_gague_max;
        right_gague = right_gague_max;
        StartCoroutine("BattleMain");
        StartCoroutine("TeamTimerGague");
    }

    // void Update(){

        
    // }

    IEnumerator BattleMain() {   
        FirstTeam();
        while(true){ // 계속반복
            first_turn = !first_turn;
            if(first_turn){
                TurnTeam("Left");
            }
            else{
                TurnTeam("Right");
            }
            if(game_cards.Count<1)
                game_cards = CardSuffle();
            DiceRoll(); // 주사위를 굴린다
            //MakeADummy(true);
            while(true){ // 모든 캐릭터에게 주사위가 있으면 진행
                if(!left_turn || !right_turn){
                    if(left_turn){
                        if(left_dice.TrueForAll(x => x.value > 0)){
                                TurnTeam("Right");
                            }
                    }
                    if(right_turn){
                        if(right_dice.TrueForAll(x => x.value > 0)){
                                TurnTeam("Left");
                            }
                    }
                    if(left_dice.TrueForAll(x => x.value > 0 && x.gameObject.activeSelf) && right_dice.TrueForAll(x => x.value > 0 && x.gameObject.activeSelf)){
                        right_turn = true;
                        left_turn = true;
                        backGround.leftCircle.SetActive(true);
                        backGround.rightCircle.SetActive(true);
                        battle_ready =  true;                    
                    }
                }
                if(battle_start)
                {
                    target1 = null;
                    target2 = null;
                    if(first_turn){
                        TurnTeam("Left");
                    }
                    else{
                        TurnTeam("Right");
                    }
                    break;
                }
                    
                yield return null;
            }
            //MakeADummy(false);
            while(!battle_end){ // 모든 캐릭터에게 주사위가 없으면 진행
                yield return null;
                if(battleing){
                    continue;
                }

                if(!battle_end){

                    if(left_dice.TrueForAll(x => x.value <= 0) &&
                    right_dice.TrueForAll(x => x.value <= 0)){
                        battle_end =  true;
                        break;
                    }
                }
                if(target1 && target2 && target1 != target2){
                    battleing = true;
                    blackScreen.SetActive(true);
                    battleCaculate.BattleMatch(target1,target2);
                }

                
            }


            PlayerGoToOrigin();
            while(camera.isZeroMove){
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            while(card_left_draw>0 || card_right_draw>0){
                
                card_getting_team = !first_turn;
                if(card_getting_team){
                    TurnTeam("Left");
                }
                else{
                    TurnTeam("Right");
                }
                if((card_getting_team && card_left_draw <= 0) || (!card_getting_team && card_right_draw <= 0)){
                    card_getting_team = !card_getting_team;
                    if(card_getting_team){
                        TurnTeam("Left");
                    }
                    else{
                        TurnTeam("Right");
                    }
                }
                
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


    IEnumerator TeamTimerGague(){
        while(true){
            if(!battleing){
                if(left_turn){
                left_gague -= 40f * Time.deltaTime;
                if(left_gague<=0){
                    TurnTeam("Right");
                }
                }
                if(right_turn){
                right_gague -= 40f * Time.deltaTime;
                if(right_gague<=0){
                    TurnTeam("Left");
                }
                }
            }


            ui.left_gague.value = left_gague/left_gague_max;
            ui.right_gague.value = right_gague/right_gague_max;
            yield return null;
        }
        
    }

    void FirstTeam(){
        int rand = (int)Random.Range(0f,2f);
        if(rand == 0){
            TurnTeam("Right");
            first_turn = true;
        }
        else{
            TurnTeam("Left");
            first_turn = false;
        }
    }

    public void TurnTeam(string team){
        if(team == "Left"){
            right_gague = (right_gague<right_gague_max) ? right_gague+100f : right_gague_max;
            backGround.TeamChanged("Left");
            left_turn = true;
            right_turn = false;
        }
        if(team == "Right"){
            left_gague = (left_gague<left_gague_max) ? left_gague+100f : left_gague_max;
            backGround.TeamChanged("Right");
            right_turn = true;
            left_turn = false;
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
        // for(int i = 0; i< dices.Count; i++)
        //     if(!players[i].died){
        //         dices[i].rolldice();
        //     }     
        foreach(Dice di in dices){
            di.gameObject.SetActive(false);
        }
        List<Dice> dice_left = dices.FindAll(x => x.tag.Equals("Team1"));
        List<Dice> dice_right = dices.FindAll(x => x.tag.Equals("Team2"));
        List<Dice_Indi> dice_list_left = left_dice.FindAll(x => x.gameObject.activeSelf);
        List<Dice_Indi> dice_list_right = right_dice.FindAll(x => x.gameObject.activeSelf);

        Debug.Log(dice_list_left.Count);
        Debug.Log(dice_list_right.Count);

        float[] cardLerps = new float[dice_list_left.Count];
        switch(dice_list_left.Count){ // 카드수에 맞게 위치 조정
            case 1: cardLerps = new float[] {0f}; break;
            case 2: cardLerps = new float[] {-1f,1f}; break;
            case 3: cardLerps = new float[] {-2f,0f,2f}; break;
            default:
                float interval = diceSort_scale / (dice_list_left.Count-1);
                for(int i = 0; i < dice_list_left.Count;i++){
                    cardLerps[i] = interval * i-diceSort_scale/2;
                } break;
            }
        for(int i = 0; i<dice_list_left.Count; i++){
            dice_left[i].gameObject.SetActive(true);
            dice_left[i].rolldice(cardLerps[i]-4f);
        }
              

        cardLerps = new float[dice_list_right.Count];
        switch(dice_list_right.Count){ // 카드수에 맞게 위치 조정
            case 1: cardLerps = new float[] {0f}; break;
            case 2: cardLerps = new float[] {-1f,1f}; break;
            case 3: cardLerps = new float[] {-2f,0f,2f}; break;
            default:
                float interval = diceSort_scale / (dice_list_right.Count-1);
                for(int i = 0; i < dice_list_right.Count;i++){
                    cardLerps[i] = interval * i-diceSort_scale/2;
                } break;
            }


        for(int i = 0; i<dice_list_right.Count; i++){
            dice_right[i].gameObject.SetActive(true);
            dice_right[i].rolldice(cardLerps[i]+4f);
        }
                   
    }

    // void MakeADummy(bool ver){
    //     if(ver == true){
    //         for(int i = 0; i< players.Count; i++)
    //             if(players[i].died){
    //                 players[i].SetDice(1);
    //             }     
    //     }      
    //     if(ver == false){
    //         for(int i = 0; i< players.Count; i++)
    //             if(players[i].died){
    //                 players[i].SetDice(0);
    //             }     
    //     }       
    // }

    void BattlePreReset(){
        DiceRoll();
        foreach(Dice_Indi dice in left_dice){
            dice.isDiced = false;
        }
        foreach(Dice_Indi dice in right_dice){
            dice.isDiced = false;
        }
        battle_start = false;
        battle_end = false;
        target1 = null;
        target2 = null;
        // target1 = 0;
        // target2 = 0;
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
