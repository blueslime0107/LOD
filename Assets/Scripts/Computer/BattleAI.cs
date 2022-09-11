using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{

    public bool did = false;

    public BattleManager bm;
    public BattleFunction bf;
    public string my_team;
    
    public string[] diceSelect_type;
    public string[] battle_type;

    public string diceSelect;
    public string battleType;

    public List<Player> my_players = new List<Player>();
    public List<Player> ene_players = new List<Player>();
    public List<Dice> my_dice = new List<Dice>();

    Battlgolithm onDiceSelect;
    Battlgolithm onBattleing;

    public void AIPreSet(){
        if(gameObject.tag.Equals("PlayerTeam1")){
            my_team = "Left";
            my_players = bm.left_players;
            ene_players = bm.right_players;
            my_dice = bm.dices.FindAll(x => x.tag.Equals("Team1"));
        }
        else{
            my_team = "Right";
            my_players = bm.right_players;
            ene_players = bm.left_players;
            my_dice = bm.dices.FindAll(x => x.tag.Equals("Team2"));
        }
    }

    public void isDiceSelect(){
        if(!bf.TeamBool2Str().Equals(my_team) || did){return;}
        switch(diceSelect){
            case "Random": DiceRandom();
            break;
            case "Casual": DiceCasual();
            break;
            case "Strong": DiceStrong();
            break;
            case "Safe": DiceSafe();
            break;
        }
        did = true;

        
        // for(int i=0;i<my_dice.Count;i++){
        //     bf.DiceToPlayer(my_dice[i],my_players[i].dice_Indi);
        // }
    }

    public void isBattleing(){
        if(!bf.TeamBool2Str().Equals(my_team)){return;}
        switch(battleType){
            case "Match": BattleMatch();
            break;

        }
        // List<Player> my_players_dice = my_players.FindAll(x => x.dice >0);
        // List<Player> ene_players_dice = ene_players.FindAll(x => x.dice >0);
        // if(my_players_dice.Count <= 0){return;}
        // bf.TargetPlayer(my_players_dice[0],ene_players_dice[0]);
    }

///////////////////////////////////////////////////////////////
#region 주사위지정명령
    void DiceCasual(){
        for(int i=0;i<my_dice.Count;i++){
            bf.DiceToPlayer(my_dice[i],my_players[i].dice_Indi);
        }
    }

    void DiceRandom(){
        List<Dice> dice = new List<Dice>();
        List<Dice> copy_dice = new List<Dice>(my_dice);
        for(int i=0;i<my_dice.Count;i++){
            int rand = (int)Random.Range(0f,copy_dice.Count);
            dice.Add(copy_dice[rand]);
            copy_dice.RemoveAt(rand);

        }
        for(int i=0;i<dice.Count;i++){
            bf.DiceToPlayer(dice[i],my_players[i].dice_Indi);
        }
    }

    void DiceStrong(){
        List<Player> player = new List<Player>(my_players);
        player.Sort(SortHealth);
        List<Dice> dice = new List<Dice>(my_dice);
        dice.Sort(SortDice);
        Debug.Log(dice.Count);
        for(int i=0;i<dice.Count;i++){
            bf.DiceToPlayer(dice[i],player[i].dice_Indi);
        }
        
    }

    void DiceSafe(){
        List<Player> player = new List<Player>(my_players);
        player.Sort(SortHealth);
        List<Dice> dice = new List<Dice>(my_dice);
        dice.Sort(SortDice);
        Debug.Log(dice.Count);
        for(int i=0;i<dice.Count;i++){
            Debug.Log(player.Count-i);
            bf.DiceToPlayer(dice[i],player[player.Count-i-1].dice_Indi);
        }
        
    }
#endregion
/////////////////////////////////////////////////////////////////////
#region 전투지정명령
    void BattleMatch(){
        List<Player> my_players_dice = my_players.FindAll(x => x.dice >0);
        List<Player> ene_players_dice = ene_players.FindAll(x => x.dice >0);
        if(ene_players_dice.Count >0){
            bf.TargetPlayer(my_players_dice[0],ene_players_dice[0]);
        }
        else{
            bf.TargetPlayer(my_players_dice[0],ene_players[(int)Random.Range(0,ene_players.Count)]);
        }
    }
#endregion
    
    private int SortHealth(Player pl1, Player pl2){
        if(pl1.health < pl2.health){
            return -1;
        }
        else if(pl1.health > pl2.health){
            return 1;
        }
        return 0;
    }

    private int SortDice(Dice pl1, Dice pl2){
        if(pl1.dice_value < pl2.dice_value){
            return -1;
        }
        else if(pl1.dice_value > pl2.dice_value){
            return 1;
        }
        return 0;
    }
}
