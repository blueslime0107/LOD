using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Test3 : CardAbility
{



    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        int count = 0;
        for(int i = 0;i<match.players.Count;i++){
            if(match.players[i].dice == player.dice+count && i != player.player_id-1){
                count += 1;
                
            }
                
        }
        player.SetDice(player.dice+count);

    }

}

