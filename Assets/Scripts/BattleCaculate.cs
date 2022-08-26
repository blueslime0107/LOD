using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager battleManager;
    public Player[] players;

    public List<CardAbility> my_ability = new List<CardAbility>();
    public List<CardAbility> ene_ability = new List<CardAbility>();

    Color black = new Color(0,0,0,1);
    Color white = new Color(255,255,255,255);

    public int damage;

    [SerializeField] Player myChar;
    [SerializeField] Player eneChar;

    Vector3 myOriginPos;

    int myNum;
    int eneNum;

    public bool card_activated;

    bool corrLock = false;

    public void BattleMatch(Dice_Indi self_dice, Dice_Indi ene_dice){

        foreach(Dice_Indi dice in battleManager.left_dice){
            if(dice != self_dice && dice != ene_dice){
                dice.render.color = black;
            }
        }
        foreach(Dice_Indi dice in battleManager.right_dice){
            if(dice != self_dice && dice != ene_dice){
                dice.render.color = black;
            }
        }
        // for(int i=0;i<battleManager.left_dice.Count+battleManager.right_dice.Count;i++){
        //     if(i != selfnum-1 && i != enenum-1){
        //         players[i].
        //     }
        // }

        myNum = self_dice.player.player_id -1;
        eneNum = ene_dice.player.player_id -1;

        players[myNum].OnMouseDown(); 
        players[eneNum].OnMouseDown(); 

        damage = 0;
        myChar = self_dice.player;
        eneChar = ene_dice.player;
        myChar.dice = self_dice;
        eneChar.dice = ene_dice;
        myOriginPos = players[myNum].transform.position;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.transform.position += Vector3.back;
        eneChar.transform.position += Vector3.back;
        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        myChar.ChangeCondition(2);      
        myChar.SetPointMove(players[eneNum].movePoint.position, 15f);
        gameManager.main_camera_ctrl.SetTargetMove(myNum,eneNum,17f);
        BasicDice();
        while(myChar.isMoving){
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("BasicAttack");
        yield return new WaitForSeconds(1f);
        corrLock = false;











        myChar.SetPointMove(myOriginPos, 15f);
        gameManager.main_camera_ctrl.SetZeroMove(17f);
        MatchFin();

        yield return null;
    }


    void BasicDice(){
        if(myChar.dice.value == 1 && eneChar.dice.value >= 6){
            myChar.AddDice(6,myChar.dice);
        }
        else if(eneChar.dice.value == 1 && myChar.dice.value >= 6){
            eneChar.AddDice(6,eneChar.dice);
        }
    }

    IEnumerator BasicAttack(){
        damage = myChar.dice.value - eneChar.dice.value;
        if(!corrLock){
            if(damage>0){

                for(int i = 0; i<my_ability.Count;i++){
                    my_ability[i].OnBattleWin(this);
                }
                myChar.UpdateActiveStat();
                while(card_activated){
                    yield return null;
                }
                corrLock = true;
                StartCoroutine(Damage(myChar,eneChar));
                // Damage(myChar,eneChar);
            }
            if(damage<0){
                damage = -damage;
                for(int i = 0; i<ene_ability.Count;i++){
                    ene_ability[i].OnBattleWin(this);
                }
                eneChar.UpdateActiveStat();
                while(card_activated){
                    yield return null;
                }
                corrLock = true;
                StartCoroutine(Damage(eneChar,myChar));          
                // Damage(eneChar,myChar);
            }
            if(damage == 0){
                corrLock = true;
                myChar.ChangeCondition(3);
                eneChar.ChangeCondition(3);
            }
        }

    }
    
    void MatchFin(){    
        for(int i =0;i<battleManager.players.Count;i++){
            if(battleManager.players[i].health <= 5 && battleManager.players[i].card_geted){
                if(i<3){
                    battleManager.card_left_draw += 1;
                    battleManager.players[i].card_geted = false;
                }
                else{
                    battleManager.card_right_draw += 1;
                    battleManager.players[i].card_geted = false;
                }
            }
        }
        for(int i =0;i<battleManager.players.Count;i++){ 
            if(battleManager.players[i].health <= 0 && battleManager.players[i].died_card_geted){
                if(i<3){
                    battleManager.card_left_draw += 1;
                    battleManager.players[i].died_card_geted = false;
                    battleManager.players[i].YouAreDead();
                }
                else{
                    battleManager.card_right_draw += 1;
                    battleManager.players[i].died_card_geted = false;
                    battleManager.players[i].YouAreDead();
                }
            }
        }
        // if(myChar.health <= 5  && myChar.card_geted){
        //     battleManager.card_draw += 1;
        //     myChar.card_geted = false;
        // }
        // if(eneChar.health <= 5  && eneChar.card_geted){
        //     battleManager.card_draw += 1;
        //     eneChar.card_geted = false;
        // }
        // for(int i=0;i<battleManager.players.Count;i++){
        //     if(i != myNum || i != eneNum){
        //         players[i].dice.render.color = new Color(255,255,255,255);
        //     }
        // }
        foreach(Dice_Indi dice in battleManager.left_dice){
            dice.render.color =  white;
        }
        foreach(Dice_Indi dice in battleManager.right_dice){
            dice.render.color =  white;
        }
            
        battleManager.blackScreen.SetActive(false);
        myChar.SetDice(0,myChar.dice);
        myChar.ChangeCondition(0);
        eneChar.SetDice(0,eneChar.dice);
        eneChar.ChangeCondition(0);
        battleManager.battleing = false;
        battleManager.target1 = null;
        battleManager.target2 = null;
        myChar.transform.position += Vector3.forward;
        eneChar.transform.position += Vector3.forward;

        if(battleManager.left_turn){
            battleManager.TurnTeam("Right");
            if(battleManager.right_dice.TrueForAll(x => x.value == 0)){
                battleManager.TurnTeam("Left");
            }
        }
        else if(battleManager.right_turn){
            battleManager.TurnTeam("Left");
            if(battleManager.left_dice.TrueForAll(x => x.value == 0)){
                battleManager.TurnTeam("Right");
            }
        }
    }

    IEnumerator Damage(Player attack, Player defender){
        for(int i = 0; i<attack.cards.Count; i++){
                attack.cards[i].OnDamageing(this,attack);
            }
        for(int i = 0; i<defender.cards.Count; i++){
                defender.cards[i].OnDamaged(this,defender);
            }
        attack.UpdateActiveStat();
        defender.UpdateActiveStat();
        while(card_activated){
            Debug.Log("Waiting");
            yield return null;
        }
        Debug.Log("Attacking");
        defender.Damage(damage,attack);
    }
}
