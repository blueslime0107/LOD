using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventBattle{
    public Player my_player;
    public DiceProperty my_dice;
    public Player ene_player;
    public int ene_dice;
}

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager bm;
    public List<Player> players = new List<Player>();
    public BattleDice battleDice;

    public BackColorEff blackScreen;

    public List<CardPack> my_ability = new List<CardPack>();
    public List<CardPack> ene_ability = new List<CardPack>();
    public List<EventBattle> eventBattles = new List<EventBattle>();

    [Space(15f), Header ("WaitTime")]
    [SerializeField][Range(0f,1f)] float diceRollTime;
    //[SerializeField] float battleBeforeTime;
    [SerializeField][Range(0f,1f)] float afterBeforeTime;
    [SerializeField][Range(0f,2f)] float battleFinTime;
    [SerializeField][Range(0f,2f)] float playerDeathTime;
    [SerializeField][Range(0f,1f)] float cardTime;
    [Space (5f)]
    [SerializeField][Range(0f,100f)] float playerMoveSpd;
    [Space (10f)]

    public SpecialPlayer specialFade;
    public SpecialAtk cur_special;

    public Damage damage = new Damage();
    public int damage_dice;

    public Player myChar;
    public Player eneChar;

    public bool enemyKnockBacked;

    public int ones_power = 1;


    public bool card_activated;

    bool coroutine_lock = true;

    void Start(){
        players = bm.players;
        coroutine_lock = true;
    }

    public void BattleMatch(Player my_target, Player ene_target){
        bm.sdm.Play("Fire1");
        blackScreen.BattleStart();
        foreach(Player player in players){
            if(player != my_target && player != ene_target){
                player.dice_Indi.gameObject.SetActive(false);
                player.hp_Indi.gameObject.SetActive(false);
            }
        }
        bm.ui.StartCoroutine("PanoraOn");

        my_target.ShowCardDeck(false,true); 
        ene_target.ShowCardDeck(false,true); 
        
        bm.left_cardLook_lock = true;
        bm.right_cardLook_lock = true;

        myChar = my_target;
        eneChar =  ene_target;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.StopAllCoroutines();
        myChar.transform.position = bm.gameManager.SetVector3z(myChar.transform.position,-1); 
        eneChar.StopAllCoroutines();
        eneChar.transform.position = bm.gameManager.SetVector3z(eneChar.transform.position,-1); 

        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        coroutine_lock = true;
        myChar.ChangeCondition(2);  
        eneChar.ChangeCondition((eneChar.dice_Indi.dice_list.Count<=0) ? 4 : 2);   

        BasicDice();
        StartCoroutine("BasicAttack");
        while(coroutine_lock){yield return null;}
        coroutine_lock = true;
        StartCoroutine("MainAttack");
        while(coroutine_lock){yield return null;}
        yield return new WaitForSeconds(afterBeforeTime); // 합이 끝나고 난 뒤의 딜레이

        StartCoroutine(MatchFin());

        yield return null;
    }


    void BasicDice(){        
        if(myChar.dice == ones_power && eneChar.dice >= 6){
            myChar.AddDice(6);
        }
        else if(eneChar.dice == ones_power && myChar.dice >= 6){
            eneChar.AddDice(6);
        }
    }

    IEnumerator BasicAttack(){
        bool farAtking = false;
        try{ // 캐릭터가 원거리? 아님 주사위 속성이 원거리
        farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt || eneChar.farAtt || eneChar.dice_Indi.dice_list[0].farAtt;}
        catch{
            if(myChar.dice_Indi.dice_list.Count > 0)
                farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt;
            else
                farAtking = myChar.farAtt;
        }
        

        if(!farAtking)
        {
            myChar.SetPointMove(eneChar.movePoint.position, playerMoveSpd);
            



        gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,playerMoveSpd);
        
        while(myChar.isMoving){
            yield return null;
        }
        
        }

        battleDice.gameObject.SetActive(true);

        bm.CardLogText("Start","[Clash Start "+myChar.character.name +"->"+eneChar.character.name+"]","#00ff08");
        for(int i = 0; i<my_ability.Count;i++){ // 합 시작시 카드 효과
            if(my_ability[i].blocked){continue;}
                    my_ability[i].ability.OnClashStart(my_ability[i],this,eneChar);
                }
        for(int i = 0; i<ene_ability.Count;i++){
            if(ene_ability[i].blocked){continue;}
                    ene_ability[i].ability.OnClashStart(ene_ability[i],this,myChar);
                }
        yield return new WaitForSeconds(diceRollTime); // 캐릭터들이 제자리에 온후 약간의 딜레이

        
        # region 데미지 결정됨
        BasicDice();
        SetDamage(myChar.dice - eneChar.dice);
        if(damage.value>0){    // 데미지 결정
            ParticleSystem particle = (myChar.gameObject.tag.Equals("PlayerTeam1")) ? battleDice.right_break : battleDice.left_break;
            particle.Play();
        }
        else if(damage.value<0){
            ParticleSystem particle = (eneChar.gameObject.tag.Equals("PlayerTeam1")) ? battleDice.right_break : battleDice.left_break;
            particle.Play();

        }
        else if(damage.Equals(0)){ // 같으면 둘다 터짐
            battleDice.left_break.Play();
            battleDice.right_break.Play();
        }
        # endregion 
        
        if(farAtking){
        # region 자신이 합을이기고 원거리 공격이 아니면 다아가기
        if(damage.value>0 && (!myChar.farAtt && !myChar.dice_Indi.dice_list[0].farAtt)){
            myChar.SetPointMove(eneChar.movePoint.position, 22f);
            gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
            
            while(myChar.isMoving){
                yield return null;
            }
        }
        # endregion
        # region 상대가 합을 이기고 원거리 공격을 가진게 아니면 자신에게 다아가기
        else if(damage.value<0 && eneChar.dice_Indi.dice_list.Count > 0){
            if(!eneChar.farAtt && !eneChar.dice_Indi.dice_list[0].farAtt){
                eneChar.SetPointMove(myChar.movePoint.position, 22f);
                gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
                
                while(eneChar.isMoving){
                     yield return null;
                }
                }
        }
        # endregion
        }

        
        yield return new WaitForSeconds(battleFinTime);

        
        

        coroutine_lock = false;
        yield return null;
        
        }
    IEnumerator MainAttack(){
        
        //damage_dice = damage;

        if(damage.value == 0){ // 무승부
            bm.CardLogText("Draw","[Draw]","#969696");
            for(int i = 0; i<my_ability.Count;i++){
                if(my_ability[i].blocked){continue;}
                my_ability[i].ability.OnClashDraw(my_ability[i],this,eneChar);
            }
            for(int i = 0; i<ene_ability.Count;i++){
                if(ene_ability[i].blocked){continue;}
                ene_ability[i].ability.OnClashDraw(ene_ability[i],this,myChar);
            }
            SetDamage(myChar.dice - eneChar.dice);
            if(damage.value != 0){
                
            }
            else{
            StartCoroutine(Damage(myChar,eneChar));
            yield return null;

            }
        }





        if(damage.value>0){ // 승리


            bm.CardLogText((myChar.team.Equals(bm.left_team)) ? "Win":"Lose","[Win "+myChar.character.name+"/Lose "+eneChar.character.name+"]","#ffffff");
            for(int i = 0; i<ene_ability.Count;i++){
                if(ene_ability[i].blocked){continue;}
                ene_ability[i].ability.OnClashLose(ene_ability[i],this, myChar);
            }
            for(int i = 0; i<my_ability.Count;i++){
                if(my_ability[i].blocked){continue;}
                my_ability[i].ability.OnClashWin(my_ability[i],this, eneChar);
                BasicDice();
            }

            // SPECIAL ATK
            if(myChar.special_active && myChar.specialAtk){
                specialFade.specialAtk = myChar.specialAtk;
                if(specialFade.specialAtk.largeAtk){
                    foreach(Player player in eneChar.team.players){
                    specialFade.specialAtk.characters.Add(player.character);

                    }
                }else{
                    specialFade.specialAtk.characters.Add(eneChar.character);
                }
                specialFade.SpeicalFade();
                while(specialFade.specialEnd){
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
                myChar.special_active = false;
                specialFade.SpecialEnd();
            }
            /////////
            StartCoroutine(Damage(myChar,eneChar));
            yield return null;
        }
        if(damage.value<0){ // 패배
        bm.CardLogText((myChar.team.Equals(bm.left_team)) ? "Win":"Lose","[Win "+eneChar.character.name+"/Lose "+myChar.character.name+"]","#ffffff");
            damage.value = -damage.value;
            for(int i = 0; i<my_ability.Count;i++){
                if(my_ability[i].blocked){continue;}
                my_ability[i].ability.OnClashLose(my_ability[i],this, eneChar);
            }
            for(int i = 0; i<ene_ability.Count;i++){
                if(ene_ability[i].blocked){continue;}
                ene_ability[i].ability.OnClashWin(ene_ability[i],this, myChar);
                BasicDice();
            }

            // SPECIAL ATK
            if(eneChar.special_active && eneChar.specialAtk){
                specialFade.specialAtk = eneChar.specialAtk;
                if(specialFade.specialAtk.largeAtk){
                    foreach(Player player in myChar.team.players){
                    specialFade.specialAtk.characters.Add(player.character);

                    }
                }
                else{
                    specialFade.specialAtk.characters.Add(myChar.character);
                }
                specialFade.SpeicalFade();
                while(specialFade.specialEnd){
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
                eneChar.special_active = false;
                specialFade.SpecialEnd();
            }
            /////////

            StartCoroutine(Damage(eneChar,myChar));   
            yield return null;       
            // Damage(eneChar,myChar);
        }



        
    }
 
    IEnumerator MatchFin(){    
        if(myChar.health <= 0){
            myChar.YouAreDead();
            if(eneChar.special_active && eneChar.specialAtk){
                specialFade.specialAtk = eneChar.specialAtk;
                if(specialFade.specialAtk.largeAtk){
                    foreach(Player player in myChar.team.players){
                    specialFade.specialAtk.characters.Add(player.character);

                    }
                }
                else{
                    specialFade.specialAtk.characters.Add(myChar.character);
                }
                specialFade.SpeicalFade();
                while(specialFade.specialEnd){
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
                eneChar.special_active = false;
                specialFade.SpecialEnd();
            }
        }
        if(eneChar.health <= 0){
            eneChar.YouAreDead();
            if(myChar.special_active && myChar.specialAtk){
                specialFade.specialAtk = myChar.specialAtk;
                if(specialFade.specialAtk.largeAtk){
                    foreach(Player player in eneChar.team.players){
                    specialFade.specialAtk.characters.Add(player.character);

                    }
                }else{
                    specialFade.specialAtk.characters.Add(eneChar.character);
                }
                specialFade.SpeicalFade();
                while(specialFade.specialEnd){
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
                myChar.special_active = false;
                specialFade.SpecialEnd();
            }
            while(eneChar.gameObject.activeSelf) {yield return null;}

        }




        bm.ui.StartCoroutine("PanoraOff");

        gameManager.main_camera_ctrl.SetZeroMove(17f);
            
        # region battleVarReset
        bm.left_cardLook_lock = false;
        bm.right_cardLook_lock = false;
        bm.ui.CardFold("Left",true);
        bm.ui.CardFold("Right",true);

        ones_power = 1;
        
        bm.target1 = null;
        bm.target2 = null;
        // myChar.transform.position = bm.gameManager.SetVector3z(myChar.transform.position,0);
        // eneChar.transform.position = bm.gameManager.SetVector3z(eneChar.transform.position,0);
        foreach(Player player in bm.players){
            if(player.dice > 0){
                player.ChangeCondition(1);
            }
            else{
                player.ChangeCondition(0);
            }
            player.transform.position = bm.gameManager.SetVector3z(player.transform.position,0);
        }

        battleDice.gameObject.SetActive(false);
        # endregion

        bm.CardLogText("End","[Clash End]","#009105");

        
        for(int i = 0; i<bm.on_battle_card_effect.Count;i++){
            bm.on_battle_card_effect[i].gameObject.SetActive(false);
            bm.on_battle_card_effect.Remove(bm.on_battle_card_effect[i]);
        }
        
        //bm.RefreshPlayerDied();
        
        
        


        
        if(bm.cardViewChar_left != null){
            bm.cardViewChar_left.player_floor_render.SetInt("_Active",1); 
            bm.cardViewChar_left.ShowCardDeck(true,true);
            bm.left_cardLook_lock = true;
        }
        if(bm.cardViewChar_right != null){
            bm.cardViewChar_right.player_floor_render.SetInt("_Active",1); 
            bm.cardViewChar_right.ShowCardDeck(true,true);
            bm.left_cardLook_lock = true;
        }
        # region BattleEnded
        for(int i =0;i<myChar.cards.Count;i++){
            if(myChar.cards[i].blocked){continue;}
            myChar.cards[i].card_battleActive = false;
            
            myChar.cards[i].ability.OnClashEnded(myChar.cards[i],this);
            
        }
        for(int i =0;i<eneChar.cards.Count;i++){
            if(eneChar.cards[i].blocked){continue;}
            eneChar.cards[i].card_battleActive = false;
            eneChar.cards[i].ability.OnClashEnded(eneChar.cards[i],this);
            
        }
        #endregion

        myChar.dice_Indi.NextDice();
        eneChar.dice_Indi.NextDice();
        # region EventBattle
        
        if(eventBattles.Count > 0){
            for(int i=0;i<eventBattles.Count;i++){
                if(eventBattles[i].my_player.died || eventBattles[i].ene_player.died){
                    eventBattles.Remove(eventBattles[i]);
                }
            }
        }
        if(eventBattles.Count > 0){
            EventBattle eventBattle = eventBattles[0];
            eventBattles.RemoveAt(0);
            eventBattle.my_player.dice_Indi.put_subDice(eventBattle.my_dice,true);
            if(eventBattle.ene_dice > -1){
                bm.MakeNewDiceAndPutPlayer(eventBattle.ene_player,eventBattle.ene_dice,true);
            }
            BattleMatch(eventBattle.my_player,eventBattle.ene_player);
            yield break; // 전투 끄기
        }
        #endregion

        foreach(Player player in bm.players){
            player.StartCoroutine("GotoOrigin");
            if(player != myChar || player != eneChar){
                player.dice_Indi.gameObject.SetActive(true);
                player.hp_Indi.gameObject.SetActive(true);
                player.dice_Indi.onMouseDown = false;
                player.dice_Indi.onMouseEnter = false;
            }
        }

        battleDice.DamageUpdate();

        myChar.ChangeCondition(0);
        eneChar.ChangeCondition(0);

        bm.CheckNextTeam();


        bm.battleing = false;
        blackScreen.BattleFin();
        yield return null;
    }

    IEnumerator Damage(Player attacker, Player defender){
        if(damage.value>0){
            defender.ChangeCondition(4);
            attacker.ChangeCondition(3);
            defender.DamagedBy(damage,attacker,attacker.character.char_sprites.atkSound);
        }
        if(damage.value.Equals(0)){
            defender.ChangeCondition(3);
            attacker.ChangeCondition(3);
            if(attacker.team == bm.left_team){
            defender.KnockBack(0.5f);
            attacker.KnockBack(-0.5f);
            }
            else{
                defender.KnockBack(-0.5f);
                attacker.KnockBack(0.5f);
            }
            bm.sdm.Play("Pery");
        }
        if(attacker.atkEffect){AtkEffectAble(attacker,defender.transform);}
        attacker.AttackEffect(defender);
        coroutine_lock = false;
        yield return null;
    }

    public void SetDamage(int value){
        damage.value = value;
        battleDice.DamageUpdate();
    }

    public void AddDamage(int value){
        damage.value += value;
        battleDice.DamageUpdate();
    }

    public void AtkEffectAble(Player player,Transform tran){
        player.atkEffect.gameObject.SetActive(true);
        if(player.gameObject.tag == "PlayerTeam1"){
            player.atkEffect.transform.eulerAngles = Vector3.zero;
        }
        else{
            player.atkEffect.transform.eulerAngles = Vector3.up*180;
        }
        if(player.character.char_sprites.farAtk){
            player.atkEffect.transform.position = tran.position;
        }
    }

    public void MakeNewEventBattle(Player atk,Player def,DiceProperty atk_next,int def_next=-1){
        EventBattle eventBattle = new EventBattle();
        eventBattle.my_player = atk;
        eventBattle.ene_player = def;
        eventBattle.my_dice = atk_next;
        eventBattle.ene_dice = def_next;
        eventBattles.Add(eventBattle);
    }

}
