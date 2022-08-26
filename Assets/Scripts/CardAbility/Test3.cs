using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Test3 : CardAbility
{



    public override void MatchStarted(Dice_Indi dice, BattleManager match)
    {
        int count = 0;
        for(int i = 0;i<match.players.Count;i++){
            if(match.players[i].dice.value == player.dice.value+count && i != player.player_id-1){
                count += 1;
                
            }
                
        }
        player.SetDice(player.dice.value+count,player.);

    }

}

