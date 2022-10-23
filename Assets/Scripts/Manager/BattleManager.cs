using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public BattleCaculate battleCaculate;
    public BattleAI Right_battleAI;
    //public BattleAI Left_battleAI;
    public new CameraCtrl camera;
    public BackGround backGround;
    public LineRenderer lineRender;
    public LineRenderer cardlineRender;
    public UI ui;
    [HideInInspector]public List<Dice> dices = new List<Dice>();
    [HideInInspector]public List<Dice_Indi> dice_indis = new List<Dice_Indi>();
    [HideInInspector]public List<Player> players = new List<Player>();
    public List<CardAbility> cards = new List<CardAbility>();
    public List<CardAbility> game_cards = new List<CardAbility>();

    public List<CardEffect> on_battle_card_effect = new List<CardEffect>();

    //public List<CardEffect> card_effect = new List<CardEffect>();

    public Cards cardViewer;
    public GameObject blackScreen;

    public bool battle_ready;
    public bool battle_start;
    public bool battle_end = false;

    [HideInInspector]public string card_getting_team;
    public int card_left_draw = 0;
    public int card_right_draw = 0;
    [HideInInspector]public bool card_gived = false;
    [HideInInspector]public int card_give_count = 0;
    public List<CardDraw> show_cards = new List<CardDraw>();

    public bool left_cardLook_lock = false;
    public bool right_cardLook_lock= false;
    [HideInInspector]public bool cardTouching = false;

    public CardPack card_selecting;
    public bool card_select_trigger;

    public CardAbility null_card;

    [HideInInspector]public Player target1;
    [HideInInspector]public Player target2;
    public Player mouseTouchingTarget;

    [HideInInspector]public bool battleing;

    public Player cardViewChar_left;
    public Player cardViewChar_right;
    public Player render_cardViewChar_left;
    public Player render_cardViewChar_right;

    public string first_turn;
    [HideInInspector]public bool right_turn;
    [HideInInspector]public bool left_turn;

    float gague_time;
    public float left_gague_max;
    public float right_gague_max;
    [SerializeField]float left_gague;
    [SerializeField]float right_gague;
    [SerializeField]float left_gague_spd;
    [SerializeField]float right_gague_spd;

    [HideInInspector]public List<Player> left_players = new List<Player>();
    [HideInInspector]public List<Player> right_players = new List<Player>();

    [HideInInspector]public bool left_d6 = false;
    [HideInInspector]public bool right_d6 = false;
    [HideInInspector]public int left_d6_Count;
    [HideInInspector]public int right_d6_Count;

    public void Battle(){
        left_gague = left_gague_max;
        right_gague = right_gague_max;
        left_players = players.FindAll(x => x.gameObject.tag.Equals("PlayerTeam1"));
        right_players = players.FindAll(x => x.gameObject.tag.Equals("PlayerTeam2"));    
        StartCoroutine("BattleMain");
        StartCoroutine("TeamTimerGague");
    }

    IEnumerator BattleMain() {   
        FirstTeam();

        Right_battleAI.AIPreSet();
        //Left_battleAI.AIPreSet();

        while(true){ // 계속반복
            if(left_players.FindAll(x => x.died).Count.Equals(left_players.Count)){
                Debug.Log("You Lose!");
                break;
            }
            if(right_players.FindAll(x => x.died).Count.Equals(right_players.Count)){
                Debug.Log("You Win!");
                gameManager.sm.stage.victoryed = true;
                break;
            }
            # region 전투끝/카드뽑기
            PlayerGoToOrigin();
            left_cardLook_lock = false;
            right_cardLook_lock = false;
            while(camera.isZeroMove){
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            while(card_left_draw>0 || card_right_draw>0){
                if(first_turn.Equals("Left")){
                        card_getting_team = "Right";
                    }
                    else{
                        card_getting_team = "Left";
                    }
                //card_getting_team = (first_turn.Equals("Left")) ? "Left" : "Right";

                if((card_getting_team.Equals("Left") && card_left_draw <= 0) || (card_getting_team.Equals("Right") && card_right_draw <= 0)){
                    if(card_getting_team.Equals("Left")){
                        card_getting_team = "Right";
                    }
                    else{
                        card_getting_team = "Left";
                    }
                    //card_getting_team = (card_getting_team.Equals("Left")) ? "Left" : "Right";
                    
                }
                Debug.Log(card_getting_team);
                    TurnTeam(card_getting_team);
                
                if(game_cards.Count<1) // 카드가 없을때 새로 다시 섞기
                    game_cards = CardSuffle();

                
                switch(game_cards.Count){ // 기본 카드 3장 뽑기
                    case 1: card_give_count = 1;break;
                    case 2: card_give_count = 2;break;
                    default: card_give_count = 3;break;
                }

                if(left_turn){ 
                    foreach(Player player in left_players){
                        for(int i = 0;i<player.cards.Count;i++){
                            player.cards[i].ability.BeforeCardDraw(player.cards[i],this,player);
                        
                        }
                    }
                    
                }
                else{
                    foreach(Player player in right_players){
                        for(int i = 0;i<player.cards.Count;i++){
                            player.cards[i].ability.BeforeCardDraw(player.cards[i],this,player);
                        
                        }
                    }
                }

                switch(card_give_count){ 
                    case 1:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,game_cards[0]);break;
                    case 2: show_cards.Clear();
                        Card(Vector3.up*9+Vector3.right*2.5f+Vector3.back,game_cards[0]);
                        Card(Vector3.up*9+Vector3.left*2.5f+Vector3.back,game_cards[0]);break;
                    case 3:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,game_cards[0]);
                        Card(Vector3.up*9+Vector3.right*5+Vector3.back,game_cards[0]);
                        Card(Vector3.up*9+Vector3.left*5+Vector3.back,game_cards[0]);  break;
                    case 5:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,game_cards[0]);
                        Card(Vector3.up*9+Vector3.right*3+Vector3.back,game_cards[0]);
                        Card(Vector3.up*9+Vector3.left*3f+Vector3.back,game_cards[0]);  
                        Card(Vector3.up*9+Vector3.right*6f+Vector3.back,game_cards[0]); 
                        Card(Vector3.up*9+Vector3.left*6f+Vector3.back,game_cards[0]); break;
                }
                ui.cardMessage.SetActive(true);
                if(left_turn){
                    if(left_d6 && left_d6_Count>0){
                    ui.Dice6.gameObject.SetActive(true);
                    }
                    else{
                        ui.Dice6.gameObject.SetActive(false);
                    }
                }
                else{
                    if(right_d6 && right_d6_Count>0){
                        ui.Dice6.gameObject.SetActive(true);
                    }
                    else{
                        ui.Dice6.gameObject.SetActive(false);
                    }
                }
                
                  
                           
                while(!card_gived){
                    yield return null;
                }
                //// 카드를 뽑은 뒤 이벤트
                if(left_turn){
                    foreach(Player player in left_players){
                        for(int i = 0;i<player.cards.Count;i++){
                            player.cards[i].ability.AfterCardDraw(this,player);
                        
                        }
                    }
                    
                }
                else{
                    foreach(Player player in right_players){
                        for(int i = 0;i<player.cards.Count;i++){
                            player.cards[i].ability.AfterCardDraw(this,player);
                        
                        }
                    }
                }
                ui.cardMessage.SetActive(false);
                card_gived = false;
            }
            ui.Dice6.gameObject.SetActive(false);
            
            
            BattlePreReset();
            # endregion
            # region 선두팀 바꾸기   
            if(first_turn.Equals("Left")){
                first_turn = "Right"; 
            }
            else{
                first_turn = "Left"; 
            }
            TurnTeam(first_turn);
            if(game_cards.Count<1)
                game_cards = CardSuffle();
            
            # endregion
            # region 주사위 굴리기
            foreach(Player player in players){
                for(int i =0; i<player.cards.Count;i++){
                    player.cards[i].ability.StartMatch(player.cards[i],this);
                }
            }
            DiceRoll(); // 주사위를                                                 
            yield return new WaitForSeconds(1f);
            Right_battleAI.did = false;
            foreach(Dice die in dices){
                die.StopRollingDice();
            }

            # endregion
            # region 주사위 지정
            while(true){ // 모든 캐릭터에게 주사위가 있으면 진행
                if(!left_turn || !right_turn){
                    // 팀이 주사위를 전부 얻으면 상대팀
                    if(left_turn){
                        if(left_players.FindAll(x => x.dice>0 || x.died).Count >= left_players.Count)
                            //(players[0].dice > 0 && players[1].dice > 0 && players[2].dice > 0){
                                TurnTeam("Right");
                            //}
                    }
                    if(right_turn){
                        if(right_players.FindAll(x => x.dice>0 || x.died).Count >= right_players.Count){
                                TurnTeam("Left");
                            }
                    }
                    
                    //Left_battleAI.isDiceSelect();
                    Right_battleAI.isDiceSelect();
                    
                    // 양팀 주사위 넣음

                    if(left_players.FindAll(x => x.dice>0 || x.died).Count >= left_players.Count &&
                    right_players.FindAll(x => x.dice>0 || x.died).Count >= right_players.Count){
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
                    if(first_turn.Equals("Left")){
                        TurnTeam("Left");
                    }
                    else{
                        TurnTeam("Right");
                    }
                    break;
                }
                    
                yield return null;
            }
            # endregion
            # region 전투시작
            while(!battle_end){ // 모든 캐릭터에게 주사위가 없으면 진행
                yield return null;

                if(battleing){
                    continue;
                }

                if(!battle_end){
                    if(left_players.FindAll(x => x.dice<=0 || x.died).Count >= left_players.Count &&
                    right_players.FindAll(x => x.dice<=0 || x.died).Count >= right_players.Count){
                        battle_end =  true;
                        break;
                    }
                    else{
                        
                    }
                }
                //Left_battleAI.isBattleing();
                Right_battleAI.isBattleing();

                if(Input.GetMouseButtonDown(0)){
                    foreach(Dice_Indi dice in dice_indis){
                        
                        if(dice.onMouseDown){
                            if(dice.gameObject.tag.Equals("Team1") && left_turn){
                                target1 = dice.player;
                            }
                            if(dice.gameObject.tag.Equals("Team2") && right_turn){
                                target1 = dice.player;
                            }
                        }
                    }

                }
                if(Input.GetMouseButtonUp(0) && target1 != null){
                    foreach(Dice_Indi dice in dice_indis){
                        if(!dice.onMouseDown && dice.onMouseEnter && dice != target1.dice_Indi){
                            target2 = dice.player;
                            BattleTargetReady();
                        }
                        
                    }
                    if(target2 == null){
                        target1 = null;
                    }
                    lineRender.SetPosition(1, Vector3.zero+Vector3.forward);
                    lineRender.SetPosition(0, Vector3.zero+Vector3.forward); 

                }

                

                
            }
            # endregion

        }   
        yield return new WaitForSeconds(1f);
        if(gameManager.sm.stage.afterStory != null){
            gameManager.sceneMove.MoveStory();
        }
        else{
            gameManager.sceneMove.MoveLobby();
        }
                 
    }


    public void BattleTargetReady(){
        if(target1 != target2){
            battleing = true;
            blackScreen.SetActive(true);

            battleCaculate.BattleMatch(target1,target2);
        }
    }

    public void ReRoll(){
        for(int i =0; i<show_cards.Count;i++){
            show_cards[i].DestroyTheCard();
        }
        show_cards.Clear();
        if(game_cards.Count<1)
            game_cards = CardSuffle();
        switch(card_give_count){
            case 1:
                Card(Vector3.up+Vector3.back,game_cards[0]);break;
            case 2: 
                Card(Vector3.up+Vector3.right*2.5f+Vector3.back,game_cards[0]);
                Card(Vector3.up+Vector3.left*2.5f+Vector3.back,game_cards[0]);break;
            case 3:
                Card(Vector3.up+Vector3.back,game_cards[0]);
                Card(Vector3.up+Vector3.right*5+Vector3.back,game_cards[0]);
                Card(Vector3.up+Vector3.left*5+Vector3.back,game_cards[0]);  break;
            case 5:
                Card(Vector3.up+Vector3.back,game_cards[0]);
                Card(Vector3.up+Vector3.right*3+Vector3.back,game_cards[0]);
                Card(Vector3.up+Vector3.left*3f+Vector3.back,game_cards[0]);  
                Card(Vector3.up+Vector3.right*6f+Vector3.back,game_cards[0]); 
                Card(Vector3.up+Vector3.left*6f+Vector3.back,game_cards[0]); break;
        }
    }

    IEnumerator TeamTimerGague(){
        while(true){
            if(!battleing){
                if(left_turn){
                left_gague -= left_gague_spd * Time.deltaTime;
                if(left_gague<=0){
                    TurnTeam("Right");
                }
                }
                if(right_turn){
                right_gague -= right_gague_spd * Time.deltaTime;
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
            first_turn = "Right";
        }
        else{
            TurnTeam("Left");
            first_turn = "Left";
        }
            TurnTeam("Left");
            first_turn = "Left";
    }

    public void TurnTeam(string team){
        if(team.Equals("Left")){
            right_gague = (right_gague<right_gague_max) ? right_gague+100f : right_gague_max;
            backGround.TeamChanged("Left");
            left_turn = true;
            right_turn = false;
        }
        if(team.Equals("Right")){
            left_gague = (left_gague<left_gague_max) ? left_gague+100f : left_gague_max;
            backGround.TeamChanged("Right");
            right_turn = true;
            left_turn = false;
        }
    }

    void PlayerGoToOrigin(){

        foreach(Player player in players){
            player.goto_origin = true;
            player.player_floor_render.SetInt("_Active",0);
        }

    }

    void DiceRoll(){
        List<Dice> dice_lerp = new List<Dice>();
        float Sort_scale = 5f;
        dice_lerp = dices.FindAll(x => x.tag.Equals("Team1"));
        foreach(Dice dice in dice_lerp){
            float[] diceLerps = new float[dice_lerp.Count];
            switch(dice_lerp.Count){ // 카드수에 맞게 위치 조정
                case 1: diceLerps = new float[] {-2f}; break;
                case 2: diceLerps = new float[] {-2f,0f}; break;
                case 3: diceLerps = new float[] {-2f,0f,2f}; break;
                default:float interval = Sort_scale / (dice_lerp.Count-1);
                    for(int i = 0; i < dice_lerp.Count;i++){
                        diceLerps[i] = interval * i-Sort_scale/2;
                    } break;
            }
            for(int i = 0; i < dice_lerp.Count; i++){ 
                dice_lerp[i].rolldice(Vector2.right*diceLerps[i]+Vector2.up*3+Vector2.left*4);
            }
        }
        dice_lerp = dices.FindAll(x => x.tag.Equals("Team2"));
        foreach(Dice dice in dice_lerp){
            float[] diceLerps = new float[dice_lerp.Count];
            switch(dice_lerp.Count){ // 카드수에 맞게 위치 조정
                case 1: diceLerps = new float[] {-2f}; break;
                case 2: diceLerps = new float[] {-2f,0f}; break;
                case 3: diceLerps = new float[] {-2f,0f,2f}; break;
                default:float interval = Sort_scale / (dice_lerp.Count-1);
                    for(int i = 0; i < dice_lerp.Count;i++){
                        diceLerps[i] = interval * i-Sort_scale/2;
                    } break;
            }
            for(int i = 0; i < dice_lerp.Count; i++){ 
                dice_lerp[i].rolldice(Vector2.right*diceLerps[i]+Vector2.up*3+Vector2.right*4); 
            }
        }

        
                      
    }

    void BattlePreReset(){
        // for(int i = 0; i< dices.Count; i++)
        //     dices[i].diceReroll();
        for(int i = 0; i< dices.Count; i++)
            dice_indis[i].isDiced = false;
        battle_start = false;
        battle_end = false;
        target1 = null;
        target2 = null;
    }

    public void BattleStart(){
        for(int i = 0; i < players.Count; i++){
            for(int j = 0; j < players[i].cards.Count; j++){
                players[i].cards[j].ability.MatchStarted(players[i].cards[j],players[i],this);
            }
        }
        if(battle_ready){
            battle_start = true;
            battle_ready = false;

        }
    }

    void Card(Vector3 pos,CardAbility cardo){
        game_cards.Remove(cardo);
        CardDraw draw = cardViewer.MakeCard();
        draw.gameObject.transform.position = pos + Vector3.up*2;
        draw.origin_position = pos;
        draw.StartCoroutine("MoveDown");
        draw.SetImage(cardo);
        show_cards.Add(draw);
        
    }

    public void SelectingCard(CardPack card){
        card_selecting = card;
        card_select_trigger = true;
        cardlineRender.gameObject.SetActive(true);
    }

    public void SelectiedCard(CardPack card){
        card_selecting.ability.CardSelected(card_selecting,card,this);
        card_selecting = null;
        card_select_trigger = false;
    }

    public CardPack GiveCard(CardAbility having_card, Player player){
        GameObject game_card = new GameObject();
        CardPack card = game_card.AddComponent<CardPack>() as CardPack;
        card.ability = having_card;
        card.max_gague = having_card.gague;
        card.battleManager = this;
        card.PreSetting(player);
        player.cards.Add(card);
        card.ability.ImmediCardDraw(card,this,player);
        return card;
    }

    public void GiveCardPack(CardPack card, Player player){
        CardPack pre_card = card;
        pre_card.PreSetting(player);
        player.cards.Add(pre_card);
        pre_card.ability.ImmediCardDraw(pre_card, this,player);
    }

    public List<CardAbility> CardSuffle(){
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
