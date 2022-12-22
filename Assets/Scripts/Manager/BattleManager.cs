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
    public List<CardAbility> cur_game_cards = new List<CardAbility>();

    public List<CardEffect> on_battle_card_effect = new List<CardEffect>();

    //public List<CardEffect> card_effect = new List<CardEffect>();

    public Cards cardViewer;
    public GameObject blackScreen;

    [HideInInspector]public bool cardActiveAble = false;
    public bool battle_start; // 버튼을 눌러 전투 시작인지
    public bool battle_end = false;

    [Space(10f), Header("CardGet")]
    [HideInInspector]public string card_getting_team;
    public int card_left_draw = 0;
    public int card_right_draw = 0;
    [HideInInspector]public bool card_gived = false;
    [HideInInspector]public int card_give_count = 0;
    public List<CardDraw> show_cards = new List<CardDraw>();

    [Space(10f), Header("CardLock")]
    public bool left_cardLook_lock = false;
    public bool right_cardLook_lock= false;
    [HideInInspector]public Card_text cardTouching;
    public CardPack card_selecting;
    public bool card_select_trigger;

    public CardAbility null_card;

    [Space(10f), Header("Target")]
    [HideInInspector]public Player target1;
    [HideInInspector]public Player target2;
    public Player mouseTouchingPlayer;

    [HideInInspector]public bool battleing;

    [Space(10f), Header("CardView")]
    public Player cardViewChar_left;
    public Player cardViewChar_right;
    public Player render_cardViewChar_left;
    public Player render_cardViewChar_right;

    [Space(10f), Header("Turn")]
    public string first_turn; // 전투시작시 처음 주사위,전투를 할지 결정
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

    public GameObject tutorial;

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

        if(Right_battleAI.active){Right_battleAI.AIPreSet();}
        //Left_battleAI.AIPreSet();

        while(true){ // 계속반복
            if(left_players.FindAll(x => x.died).Count.Equals(left_players.Count)){
                Debug.Log("You Lose!");
                break;
            }
            if(right_players.FindAll(x => x.died).Count.Equals(right_players.Count)){
                Debug.Log("You Win!");
                gameManager.sm.play_stage.victoryed = true;
                break;
            }
            # region 전투끝/카드뽑기
            cardActiveAble = false;
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
                }

                TurnTeam(card_getting_team);
                
                if(game_cards.Count<1) // 카드가 없을때 새로 다시 섞기
                    game_cards = CardSuffle();

                
                switch(game_cards.Count){ // 기본 카드 3장 뽑기
                    case 1: card_give_count = 1;break;
                    case 2: card_give_count = 2;break;
                    default: card_give_count = 3;break;
                }
                cur_game_cards.Clear();
                for(int i=0;i<card_give_count;i++){
                    cur_game_cards.Add(game_cards[0]);
                    game_cards.RemoveAt(0);
                }

                foreach(Player player in (left_turn) ? left_players : right_players){
                    for(int i = 0;i<player.cards.Count;i++){
                        player.cards[i].ability.BeforeCardDraw(player.cards[i],this,player);
                    
                    }
                }


                if((right_turn) && Right_battleAI.active){
                    Right_battleAI.isGettingCard(cur_game_cards);

                    card_gived = true;
                    card_right_draw -= 1;
                }
                else{
                    switch(card_give_count){ 
                    case 1:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,cur_game_cards[0]);break;
                    case 2: show_cards.Clear();
                        Card(Vector3.up*9+Vector3.right*2.5f+Vector3.back,cur_game_cards[0]);
                        Card(Vector3.up*9+Vector3.left*2.5f+Vector3.back,cur_game_cards[1]);break;
                    case 3:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,cur_game_cards[0]);
                        Card(Vector3.up*9+Vector3.right*5+Vector3.back,cur_game_cards[1]);
                        Card(Vector3.up*9+Vector3.left*5+Vector3.back,cur_game_cards[2]);  break;
                    case 5:show_cards.Clear();
                        Card(Vector3.up*9+Vector3.back,cur_game_cards[0]);
                        Card(Vector3.up*9+Vector3.right*3+Vector3.back,cur_game_cards[1]);
                        Card(Vector3.up*9+Vector3.left*3f+Vector3.back,cur_game_cards[2]);  
                        Card(Vector3.up*9+Vector3.right*6f+Vector3.back,cur_game_cards[3]); 
                        Card(Vector3.up*9+Vector3.left*6f+Vector3.back,cur_game_cards[4]); break;
                    }
                }
                
                ui.cardMessage.SetActive(true);
                # region 팀 주사위6 활성화 됨?
                if(left_turn){
                    ui.Dice6.gameObject.SetActive(left_d6 && left_d6_Count>0);
                }
                else{
                    ui.Dice6.gameObject.SetActive(right_d6 && right_d6_Count>0);
                }
                # endregion
                
                  
                           
                while(!card_gived){
                    yield return null;
                }
                //// 카드를 뽑은 뒤 이벤트
                foreach(Player player in (left_turn) ? left_players : right_players){
                    for(int i = 0;i<player.cards.Count;i++){
                        player.cards[i].ability.AfterCardDraw(this,player);
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
            if(Right_battleAI.active){Right_battleAI.did = false;}
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
                            TurnTeam("Right");
                    }
                    if(right_turn){
                        if(right_players.FindAll(x => x.dice>0 || x.died).Count >= right_players.Count)
                            TurnTeam("Left");
                    }
                    
                    //Left_battleAI.isDiceSelect();
                    if(Right_battleAI.active){Right_battleAI.isDiceSelect();}
                

                    // 주사위를 다 넣었으면(죽으면 넣은걸로 인정)
                    if(left_players.FindAll(x => x.dice>0 || x.died).Count >= left_players.Count &&
                    right_players.FindAll(x => x.dice>0 || x.died).Count >= right_players.Count){
                        cardActiveAble = true;
                        backGround.leftCircle.SetActive(true);
                        backGround.rightCircle.SetActive(true);
                        // 주사위를 다 넣었을때 효과 발동
                        for(int i = 0; i < players.Count; i++){
                            for(int j = 0; j < players[i].cards.Count; j++){
                                players[i].cards[j].ability.OnBattleStart(players[i].cards[j],players[i],this);
                            }
                        }                  
                        while(!battle_start){
                            yield return null; 
                        }
                    }
                }
                if(battle_start)
                {
                    for(int i = 0; i < players.Count; i++){
                            for(int j = 0; j < players[i].cards.Count; j++){
                                players[i].cards[j].ability.OnBattleThro(players[i].cards[j],players[i],this);
                            }
                        }
                    target1 = null;
                    target2 = null;
                    if(first_turn.Equals("Left")) TurnTeam("Left");
                    else TurnTeam("Right");
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
                if(Right_battleAI.active){Right_battleAI.isBattleing();}

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
        if(gameManager.sm.play_stage.victoryed && !gameManager.sm.play_stage.noPrice)
        {
            if(gameManager.sm.play_stage.priceStage.Count > 0){
            gameManager.sm.AddStageFun(gameManager.sm.play_stage.priceStage);
            }
            if(gameManager.sm.play_stage.priceChars.Count > 0){
                gameManager.sm.AddPlayerCardChar(gameManager.sm.play_stage.priceChars);
            }
            gameManager.sm.play_stage.noPrice = true;
        }
        if(gameManager.sm.play_stage.afterStory != null){
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
        card_gived = true;
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
        if((int)Random.Range(0f,2f) == 0){
            first_turn = "Right";
        }
        else{
            first_turn = "Left";
        }
        first_turn = "Right";
        TurnTeam(first_turn);
        
    }

    public void TurnTeam(string team = ""){
        // 팀을 바꾸면 게이지를 약간 채우고 배경을 그 팀 전용으로 맞춤한다.
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
        // 적지 않았을때 팀을 넘김
        if(team.Equals("")){
            if(left_turn){
                TurnTeam("Right");
            }
            if(right_turn){
                TurnTeam("Left");
            }

        }
    }

    void PlayerGoToOrigin(){
        // 플레이어가 모두 돌아가는걸 허락함, 바닥 활성화를 끔
        foreach(Player player in players){
            player.StartCoroutine("GotoOrigin");
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

    // 버튼 외부 발동
    public void BattleStart(){
        if(left_players.FindAll(x => x.dice>0 || x.died).Count >= left_players.Count &&
            right_players.FindAll(x => x.dice>0 || x.died).Count >= right_players.Count){ // 주사위를 
            battle_start = true;
        }
    }

    void Card(Vector3 pos,CardAbility cardo){
        CardDraw draw = cardViewer.MakeCard();
        draw.gameObject.transform.position = pos + Vector3.up*2;
        draw.origin_position = pos;
        draw.StartCoroutine("MoveDown");
        draw.SetImage(cardo);
        show_cards.Add(draw);
        
    }

    public void SelectingCard(CardPack card){
        if(!cardActiveAble){return;}
        card_selecting = card;
        card_select_trigger = true;
        cardlineRender.SetPosition(0, camera.camer.ScreenToWorldPoint(Input.mousePosition)+Vector3.forward);
        cardlineRender.gameObject.SetActive(true);
    }

    public void SelectingPlayer(CardPack card){
        if(!cardActiveAble){return;}
        card_selecting = card;
        cardlineRender.SetPosition(0, camera.camer.ScreenToWorldPoint(Input.mousePosition)+Vector3.forward);
        cardlineRender.gameObject.SetActive(true);
        StartCoroutine("selectingplayer");
    }

    IEnumerator selectingplayer(){
        while(!Input.GetMouseButtonUp(0)){yield return null;}
        try
        {card_selecting.ability.PlayerSelected(card_selecting,mouseTouchingPlayer,this);
        card_selecting = null;
        cardlineRender.gameObject.SetActive(false);
        }
        catch{

        }
        yield return null;
    }

    public void SelectiedCard(CardPack card){
        card_selecting.ability.CardSelected(card_selecting,card,this);
        card_selecting = null;
        card_select_trigger = false;
        cardlineRender.gameObject.SetActive(false);
    }

    public CardPack GiveCard(CardAbility having_card, Player player){
        GameObject game_card = new GameObject();
        CardPack card = game_card.AddComponent<CardPack>() as CardPack;
        //CardPack card = new CardPack();
        card.ability = having_card;
        card.price = having_card.price;
        card.battleManager = this;
        card.PreSetting(player);
        player.cards.Add(card);
        player.cardGet.SetActive(false);
        player.cardGet.SetActive(true);
        foreach(Player playe in players){
                for(int i =0; i<playe.cards.Count;i++){
                    playe.cards[i].ability.WhenCardGet(playe.cards[i],this,player);
                }
            }
        card.ability.WhenCardGetImmedi(card,this);
        return card;
    }

    public void GiveCardPack(CardPack card, Player player){
        CardPack pre_card = card;
        pre_card.PreSetting(player);
        player.cards.Add(pre_card);
        pre_card.ability.WhenCardGet(pre_card, this,player);
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

    public void AddCardPoint(string team){
        if(team.Equals("PlayerTeam1")){
            card_left_draw += 1;
        }
        if(team.Equals("PlayerTeam2")){
            card_right_draw += 1;
        }
    }

}
