using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{

    public BattleManager bm;
    public BattleFunction bf;
    public string my_team;
    

    public List<Player> my_players = new List<Player>();
    public List<Player> ene_players = new List<Player>();
    public List<Dice> my_dice = new List<Dice>();

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
        if(!bf.TeamBool2Str().Equals(my_team)){return;}

        for(int i=0;i<my_dice.Count;i++){
            bf.DiceToPlayer(my_dice[i],my_players[i].dice_Indi);
        }
    }

    public void isBattleing(){
        if(!bf.TeamBool2Str().Equals(my_team)){return;}
        
        List<Player> my_players_dice = my_players.FindAll(x => x.dice >0);
        List<Player> ene_players_dice = ene_players.FindAll(x => x.dice >0);
        if(my_players_dice.Count <= 0){return;}

        bf.TargetPlayer(my_players_dice[0],ene_players_dice[0]);
    }

    
    
}
