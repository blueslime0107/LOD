using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public SoundManager sdm;
    public GameManager gameManager;
    public BattleCaculate battleCaculate;
    public NumberManager numberManager;
    //public BattleAI Right_battleAI;
    //public BattleAI Left_battleAI;
    public new CameraCtrl camera;
    public BackGround backGround;
    public BezierCurve lineRender;
    public BezierCurve cardlineRender;
    public UI ui;
    [HideInInspector]public List<Dice> dices = new List<Dice>();
    [HideInInspector]public List<Dice_Indi> dice_indis = new List<Dice_Indi>();
    [HideInInspector]public List<Player> players = new List<Player>();

    public Team left_team = new Team();
    public Team right_team = new Team();
    public Team cur_team = new Team();

    public List<CardAbility> cards = new List<CardAbility>();
    public List<CardAbility> game_cards = new List<CardAbility>();
    public List<CardAbility> cur_game_cards = new List<CardAbility>();

    public List<CardEffect> on_battle_card_effect = new List<CardEffect>();

    //public List<CardEffect> card_effect = new List<CardEffect>();

    public Cards cardViewer;

    [HideInInspector]public bool cardActiveAble = false;
    public bool battle_start; // 버튼을 눌러 전투 시작인지
    public bool battle_end = false;

    [Space(10f), Header("CardGet")]
    public Team card_getting_team;
    [HideInInspector]public bool card_gived = false;
    [HideInInspector]public int card_give_count = 0;
    public List<CardDraw> show_cards = new List<CardDraw>();

    [Space(10f), Header("CardLock")]
    public bool left_cardLook_lock = false; //
    public bool right_cardLook_lock= false;
    [HideInInspector]public Card_text cardTouching;
    public CardPack card_selecting;
    public bool card_select_trigger;

    public CardAbility null_card;

    [Space(10f), Header("Target")]
    [HideInInspector]public Player target1;
    [HideInInspector]public Player target2;
    public Player mouseTouchingPlayer;
    public Card_text mouseTouchingCard;

    [HideInInspector]public bool battleing;

    [Space(10f), Header("CardView")]
    public Player cardViewChar_left; //
    public Player cardViewChar_right;
    public Player render_cardViewChar_left; //
    public Player render_cardViewChar_right;

    [Space(10f), Header("Turn")]
    public Team first_turn; // 전투시작시 처음 주사위,전투를 할지 결정

    float gague_time;
    public float left_gague_max;
    public float right_gague_max;
    [SerializeField]float left_gague;
    [SerializeField]float right_gague;
    [SerializeField]float left_gague_spd;
    [SerializeField]float right_gague_spd;

    [HideInInspector]public bool left_d6 = false;
    [HideInInspector]public bool right_d6 = false;
    [HideInInspector]public int left_d6_Count;
    [HideInInspector]public int right_d6_Count;
    public BackColorEff backColorEff;
    [SerializeField]AchiveManager achiveManager;

    public float borderX = 11;

    public GameObject tutorial;
    public void Battle(){
        // left_team.gague = left_team.max_gague;
        // right_team.gague = right_team.max_gague;
        left_team.players = players.FindAll(x => x.gameObject.tag.Equals("PlayerTeam1"));
        right_team.players = players.FindAll(x => x.gameObject.tag.Equals("PlayerTeam2"));    
        StartCoroutine("BattleMain");
        //StartCoroutine("TeamTimerGague");
    }

    IEnumerator BattleMain() {   
        FirstTeam();

        if(right_team.battleAI.active){right_team.battleAI.AIPreSet();}
        //Left_battleAI.AIPreSet();

        while(true){ // 계속반복
            if(left_team.players.FindAll(x => x.died).Count.Equals(left_team.players.Count)){
                break;
            }
            if(right_team.players.FindAll(x => x.died).Count.Equals(right_team.players.Count)){
                gameManager.sm.play_stage.victoryed = true;
                break;
            }
            # region 전투끝/카드뽑기
            cardActiveAble = false;
            // StartCoroutine("selectingCard");
            sdm.Play("LowBack");
            ui.battleStartButton_posing.MoveToMove();
            PlayerGoToOrigin();
            
            left_cardLook_lock = false;
            right_cardLook_lock = false;
            while(camera.isZeroMove){
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            while(left_team.carddraw>0 || right_team.carddraw>0){
                if(first_turn.Equals("Left")){card_getting_team = right_team;}else{card_getting_team = left_team;}
                //card_getting_team = (first_turn.Equals("Left")) ? "Left" : "Right";
                if(left_team.players.FindAll(x => x.died).Count.Equals(left_team.players.Count) || right_team.players.FindAll(x => x.died).Count.Equals(right_team.players.Count)){
                    battle_end =  true;
                    break;
                }
                if(card_getting_team.carddraw <= 0){card_getting_team = (card_getting_team.Equals(left_team)) ? right_team:left_team;}

                TurnTeam(card_getting_team);
                
                if(game_cards.Count<1) // 카드가 없을때 새로 다시 섞기
                    game_cards = CardSuffle();

                
                switch(game_cards.Count){ // 기본 카드 3장 뽑기
                    case 1: card_give_count = 1;break;
                    case 2: card_give_count = 2;break;
                    default: card_give_count = 3;break;
                }
                cur_game_cards.Clear();

                if(card_getting_team.cardGetSituations.Count > 0){
                    
                    card_give_count = card_getting_team.cardGetSituations[0].specialCards.Count;
                    for(int i=0;i<card_give_count;i++){
                    cur_game_cards.Add(card_getting_team.cardGetSituations[0].specialCards[0]);
                    card_getting_team.cardGetSituations[0].specialCards.RemoveAt(0);
                    }
                    card_getting_team.cardGetSituations.RemoveAt(0);

                }
                else{
                    for(int i=0;i<card_give_count;i++){
                    cur_game_cards.Add(game_cards[0]);
                    game_cards.RemoveAt(0);
                    }
                }

                if(cur_team.battleAI.active){
                    cur_team.battleAI.isGettingCard(cur_game_cards);
                    card_gived = true;
                    AddCardPoint(cur_team, -1);
                }
                else{
                    sdm.Play("Paper3");
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
                ui.DiceObj.SetActive(card_getting_team.diceRollGague > 0);
                # endregion
                
                  
                           
                while(!card_gived){
                    yield return null;
                }
                //// 카드를 뽑은 뒤 이벤트
                ui.cardMessage.SetActive(false);
                card_gived = false;
            }

            ui.DiceObj.SetActive(false);
            
            StopCoroutine("selectingCard");
            ui.battleStartButton_posing.MoveToOrigin();
            ui.VisualCardPanel(true);
            BattlePreReset();
            # endregion
            # region 선두팀 바꾸기   
            first_turn = (first_turn.Equals(left_team)) ? right_team : left_team; 
            ui.battleStartButton.updateYeamColor(first_turn.text);
            TurnTeam(first_turn);
            if(game_cards.Count<1)
                game_cards = CardSuffle();
            
            # endregion
            # region 주사위 굴리기
            foreach(Player player in players){
                for(int i =0; i<player.cards.Count;i++){
                    if(player.cards[i].blocked){continue;}
                    player.cards[i].ability.StartMatch(player.cards[i],this);
                }
            }
            DiceRoll(); // 주사위를                                                 
            yield return new WaitForSeconds(1f);
            if(right_team.battleAI.active){right_team.battleAI.did = false;}
            foreach(Dice die in dices){
                die.StopRollingDice();
            }
            # endregion
            # region 주사위 지정
            while(true){ // 모든 캐릭터에게 주사위가 있으면 진행

                // 팀이 주사위를 전부 얻으면 상대팀
                if(cur_team.players.FindAll(x => x.dice>0 || x.died).Count >= cur_team.players.Count)
                TurnTeam(cur_team,true);
                
                //Left_battleAI.isDiceSelect();
                if(right_team.battleAI.active){right_team.battleAI.isDiceSelect();}
            

                // 주사위를 다 넣었으면(죽으면 넣은걸로 인정)
                if(left_team.players.FindAll(x => x.dice>0 || x.died).Count >= left_team.players.Count &&
                right_team.players.FindAll(x => x.dice>0 || x.died).Count >= right_team.players.Count){
                    cardActiveAble = true;
                    backGround.leftCircle.SetActive(true);
                    backGround.rightCircle.SetActive(true);
                    // 주사위를 다 넣었을때 효과 발동
                    ui.cardLog.text = "";
                    CardLogText("BattleReady","[Battle Ready]","#ffa1fc");
                    for(int i = 0; i < players.Count; i++){
                        for(int j = 0; j < players[i].cards.Count; j++){
                            if(players[i].cards[j].blocked){continue;}
                            players[i].cards[j].ability.OnBattleReady(players[i].cards[j],players[i],this);
                        }
                    }   
                    sdm.Play("BattleReady");
                    ui.battleStartButton.StartCoroutine("startBlink");       


                    if(right_team.battleAI.active && first_turn == right_team){
                        foreach(Player player in right_team.players){
                            foreach(CardPack card in player.cards){
                                if(card.blocked){continue;}
                                card.ability.AIgorithm(card,this);
                            }
                        }
                    }

                    while(!battle_start){
                        yield return null; 
                    }

                    if(right_team.battleAI.active && first_turn == left_team){
                        foreach(Player player in right_team.players){
                            foreach(CardPack card in player.cards){
                                if(card.blocked){continue;}
                                card.ability.AIgorithm(card,this);
                            }
                        }
                    }
                }
                
                if(battle_start)
                {
                    CardLogText("BattleStart","[Battle Start]","#ff00f7");
                    for(int i = 0; i < players.Count; i++){
                            for(int j = 0; j < players[i].cards.Count; j++){
                                if(players[i].cards[j].blocked){continue;}
                                players[i].cards[j].ability.OnBattleStart(players[i].cards[j],players[i],this);
                            }
                        }
                    target1 = null;
                    target2 = null;
                    TurnTeam(first_turn);
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
                    if(left_team.players.FindAll(x => x.dice<=0 || x.died).Count >= left_team.players.Count &&
                    right_team.players.FindAll(x => x.dice<=0 || x.died).Count >= right_team.players.Count){
                        battle_end =  true;
                        break;
                    }
                }

                if(left_team.players.FindAll(x => x.died).Count.Equals(left_team.players.Count) || right_team.players.FindAll(x => x.died).Count.Equals(right_team.players.Count)){
                    battle_end =  true;
                    break;
                }

                bool rightNo = right_team.players.FindAll(x => x.dice > 0).Count > 0;
                bool leftNo = left_team.players.FindAll(x => x.dice > 0).Count > 0;

                if(cur_team.Equals(left_team)){
                    if(rightNo && !leftNo)
                        TurnTeam(right_team);
                }
                else{
                    if(leftNo && !rightNo)
                        TurnTeam(left_team);
                }

                

                //Left_battleAI.isBattleing();
                if(right_team.battleAI.active){right_team.battleAI.isBattleing();}

                if(Input.GetMouseButtonDown(0)){
                    target2 = null;
                    foreach(Dice_Indi dice in dice_indis){
                        
                        if(dice.onMouseDown){
                            if(dice.player.dice <= 0){continue;}
                            if(cur_team.players.Contains(dice.player))                       
                            target1 = dice.player;
                            
                            sdm.Play("Select1");
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
                    lineRender.gameObject.SetActive(false);

                }
                
            }
            
            CardLogText("BattleEnd","[Battle End]","#70006d");
            foreach(Player player in players){
                    for(int i = 0;i<player.cards.Count;i++){
                        if(player.cards[i].blocked){continue;}
                        player.cards[i].ability.OnBattleEnd(player.cards[i],player,this);
                    }
                }
            
            
            
            # endregion

        }   
        yield return new WaitForSeconds(1f);
        foreach(AchieveMent achieveMent in gameManager.sm.achiveItms){
            achieveMent.isStack();
        }
        if(gameManager.sm.play_stage.victoryed && !gameManager.sm.play_stage.noPrice)
        {
            if(gameManager.sm.play_stage.priceStage.Count > 0){
            gameManager.sm.AddStageFun(gameManager.sm.play_stage.priceStage);
            }
            if(gameManager.sm.play_stage.priceChars.player_Characters_id > 0){
                gameManager.sm.AddPlayerCardChar(gameManager.sm.play_stage.priceChars);
            }
            if(gameManager.sm.play_stage.priceCards.Count > 0){
                gameManager.sm.collected_card.AddRange(gameManager.sm.play_stage.priceCards);
            }
            gameManager.sm.play_stage.noPrice = true;
        }
        gameManager.sm.saveManager.Save();
        if(gameManager.sm.play_stage.after_story != null && gameManager.sm.play_stage.victoryed){
            gameManager.sm.playStory = gameManager.sm.play_stage.after_story;
            gameManager.sceneMove.Move("Talk");
        }
        else{
            gameManager.sceneMove.Move("Lobby");
        }
                 
    }

    public void CheckNextTeam(){

        if(cur_team.Equals(left_team)){
            if(right_team.players.FindAll(x => x.dice <= 0 || x.died).Count >= right_team.players.Count) TurnTeam(left_team); 
            else TurnTeam(right_team);
        }
        else{
            if(left_team.players.FindAll(x => x.dice <= 0 || x.died).Count >= left_team.players.Count) TurnTeam(right_team);
            else TurnTeam(left_team);
            
        }
    }

    public void BattleTargetReady(){
        if(target1.died || !target1.gameObject.activeSelf || target2.died || !target2.gameObject.activeSelf){
            target2 = null;
            return;
        }
        foreach(Player player in players){
            foreach(CardPack card in player.cards){
                if(card.blocked){continue;}
                card.ability.OnClashTargetSelected(card,target2,this);
            }
        }
        if(target1 != target2){

            battleing = true;

            battleCaculate.BattleMatch(target1,target2);
        }
    }

    public void ChangeTarget2(Player target){
        if(target.died || !target.gameObject.activeSelf){return;}
        target2 = target;
    }

    public void ReRoll(){
        if(card_getting_team.diceRollGague <= 0){return;}
        for(int i =0; i<show_cards.Count;i++){
            show_cards[i].DestroyTheCard();
        }
        card_gived = true;
        card_getting_team.diceRollGague -= 1;
    }

    void FirstTeam(){
        // if((int)Random.Range(0f,2f) == 0){
        //     first_turn = "Right";
        // }
        // else{
        //     first_turn = "Left";
        // }
        first_turn = right_team;
        ui.battleStartButton.updateYeamColor(first_turn.text);
        TurnTeam(first_turn);
        
    }

    public void TurnTeam(Team team,bool reverse = false){
        // 팀을 바꾸면 게이지를 약간 채우고 배경을 그 팀 전용으로 맞춤한다.
        //team.gague = (team.gague<team.max_gague) ? team.gague+100 : team.max_gague;
        if(team.Equals(cur_team) && reverse){TurnTeam((cur_team.Equals(left_team)) ? right_team:left_team);
        return;}
        backGround.TeamChanged(team.text);
        cur_team = team;
        // 적지 않았을때 팀을 넘김
    }

    void PlayerGoToOrigin(){
        // 플레이어가 모두 돌아가는걸 허락함, 바닥 활성화를 끔
        foreach(Player player in players){
            if(!player.gameObject.activeSelf){continue;}
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
        if(!cardActiveAble){return;}
        sdm.Play("Snap");
        foreach(Player player in players){
            player.ChangeCondition(1);
        }
        battle_start = true;
        ui.battleStartButton.StopAllCoroutines();
        ui.battleStartButton.updateYeamColor(first_turn.text);
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
        StartCoroutine(selectingCard());
    }

    public void SelectingPlayer(CardPack card){
        if(!cardActiveAble){return;}
        card_selecting = card;
        StartCoroutine(selectingplayer());
    }

    IEnumerator selectingplayer(){
        cardlineRender.gameObject.SetActive(true);
        while(!Input.GetMouseButtonUp(0)){
            cardlineRender.SetStart(mouseTouchingCard.transform.position,1);
            if(mouseTouchingPlayer){
                cardlineRender.SetEnd(mouseTouchingPlayer.dice_Indi.transform.position,1);
            }
            else{
                cardlineRender.SetEnd(camera.camer.ScreenToWorldPoint(Input.mousePosition),1);
            }
            yield return null;      
        }
        if(mouseTouchingPlayer == null){yield break;}
        if(card_selecting.blocked){yield break;}
        card_selecting.ability.PlayerSelected(card_selecting,mouseTouchingPlayer,this);
        card_selecting = null;
        cardlineRender.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator selectingCard(){
        cardlineRender.gameObject.SetActive(true);
        while(!Input.GetMouseButtonUp(0)){
            if(mouseTouchingCard)
            cardlineRender.SetStart(mouseTouchingCard.transform.position,1);
            if(cardTouching){
                if(cardTouching != mouseTouchingCard)
                cardlineRender.SetEnd(cardTouching.transform.position,1);
                else
                cardlineRender.SetEnd(camera.camer.ScreenToWorldPoint(Input.mousePosition),1);


            }
            else{
                cardlineRender.SetEnd(camera.camer.ScreenToWorldPoint(Input.mousePosition),1);
            }
            yield return null;      
        }
        if(cardTouching == null){yield break;}
        if(card_selecting.blocked){yield break;}
        card_selecting.ability.CardSelected(card_selecting,cardTouching.card,this);
        card_selecting = null;
        cardlineRender.gameObject.SetActive(false);
        yield return null;
    }

///////////////////////////////
    public CardPack GiveCard(CardAbility having_card, Player player,bool ableTain = false,bool cardgetEventPASS = false){
        if(having_card.tained && !ableTain){return null;}
        //GameObject game_card = new GameObject();
        CardPack card = new CardPack();
        //CardPack card = new CardPack();
        card.ability = having_card;
        card.price = having_card.price;
        card.battleManager = this;
        card = gameManager.PreSetting(card,player);
        player.cards.Add(card);
        player.cardGet.SetActive(false);
        player.cardGet.SetActive(true);

        



        if(!cardgetEventPASS){
        foreach(Player playe in players){
            for(int i =0; i<playe.cards.Count;i++){
                if(playe.cards[i].blocked){continue;}
                playe.cards[i].ability.WhenCardGet(playe.cards[i],this,player,card);
            }
        }
        }
        card.ability.WhenCardGetImmedi(card,this);
        sdm.Play("GetCard");
        return card;
    }

    public CardPack GiveCardPack(CardPack card, Player player,bool ableTain = false){
        if(card.ability.tained && !ableTain){return card;}
        Debug.Log("GiveCardPAck");
        DestroyCard(card, card.player);
        card = gameManager.PreSetting(card,player);
        player.cards.Add(card);
        card.ability.WhenCardGetImmedi(card,this);
        return card;
    }

    public void BlockCard(CardPack card){
        card.ability.WhenCardDestroy(card,this);
        foreach(CardEffect cardEffect in card.effect){
            cardEffect.gameObject.SetActive(false);
        }
        card.blocked = true;
    }
    public void UnBlockCard(CardPack card){
        card.blocked = false;
        foreach(CardEffect cardEffect in card.effect){
            cardEffect.gameObject.SetActive(true);
        }
    }

    public void DestroyCard(CardPack card, Player player){
        card.ability.WhenCardDestroy(card,this);
        player.cards.Remove(card);
    }

    

//////////////////////////////
    public Team OpposeTeam(Team team){
        if(team.Equals(left_team)){
            return right_team;
        }
        else{
            return left_team;
        }
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

    public void AddCardPoint(Team team,int count=1){
        team.carddraw += count;
        ui.CardDrawUpdate();
    }

    public void SpecialCardGet(Team team, List<CardAbility> cards){
        CardGetSituation cardGetSituation = new CardGetSituation();
        cardGetSituation.specialCards.AddRange(cards);
        team.cardGetSituations.Add(cardGetSituation);
    }

    public void CardLog(string tag, CardPack card, Player oppose = null){

        foreach(AchieveMent achieveMent in gameManager.sm.achiveItms){
            if(achieveMent.achieved){continue;}
            achieveMent.cardPack = card;
            achieveMent.tag = tag;
            achieveMent.OnCardActive();
        }
        achiveManager.RenderAchieveText();



        string color1 = (card.player.team.text.Equals("Left")) ? "#0099ff" : "#ff2b2b";
        string newText = "";

        if(oppose == null){
            newText = "<color=" + color1 + ">" + card.player.GetCharName() + "</color> " + card.ability.name + " ("+tag+")"+"\n";
            //newText = card.player.character.name + " " + card.ability.name + "\n";
            ui.cardLog.text = newText + ui.cardLog.text;

            return;
        }


        string color2 = (oppose.team.text.Equals("Left")) ? "#0099ff" : "#ff2b2b";
        newText = "<color=" + color1 + ">" + card.player.GetCharName() + "</color> " + card.ability.name + " ("+tag+")"+"\n -> <color=" + color2 + ">" + oppose.GetCharName() + "</color>" + "\n";
        //newText = card.player.character.name + "-> " + oppose.character.name + "\n -> " + card.ability.name + "\n";
        ui.cardLog.text = newText + ui.cardLog.text;
    }

    public void CardLogText(string tag, string text,string color="green"){
        foreach(AchieveMent achieveMent in gameManager.sm.achiveItms){
            if(achieveMent.achieved){continue;}
            achieveMent.tag = tag;
            achieveMent.OnBattleFoward();
        }
        achiveManager.RenderAchieveText();
        string newText = "";
        newText = "<color="+color+">" + text + "</color> \n";
        ui.cardLog.text = newText + ui.cardLog.text;
    }
    
    public int GetHealthAverage(){
        int newint =0;
        foreach(Player player in players){
            newint += player.health;
        }
        newint /= players.Count;
        return newint;
    }

    public int GetDiceAverage(){
        int newint =0;
        foreach(Player player in players){
            newint += player.dice;
        }
        newint /= players.Count;
        return newint;
    }

    public void MakeNewDiceAndPutPlayer(Player player,int value, bool atFirst=false,bool far=false){
        DiceProperty copyDice = new DiceProperty();
        copyDice.value = value;
        copyDice.farAtt = far;
        player.dice_Indi.put_subDice(copyDice,atFirst);
    }

    public DiceProperty MakeNewDice(int value,bool far=false){
        DiceProperty copyDice = new DiceProperty();
        copyDice.value = value;
        copyDice.farAtt = far;
        return copyDice;
    }
    
    
    // public void RefreshPlayerDied(){
    //     for(int i=0;i<players.Count;i++){
    //         if(i >= players.Count){return;}
    //         if(players[i].died){
    //             players.RemoveAt(i);
    //             players[i].team.players.Remove(players[i]);
    //             i--;
    //         }
    //     }
    // }

}


