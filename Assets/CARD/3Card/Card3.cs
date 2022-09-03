using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/3완벽할 모범생", order = 3)]
public class Card3 : CardAbility
{

    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        int count = 0;
        for(int i = 0;i<match.players.Count;i++){
            for(int j= 0;j<match.players.Count;j++){
            if(match.players[j].dice == player.dice+count && j != player.player_id-1){
                count += 1;
                
            }
                
        }
                
        }
        player.SetDice(player.dice+count);

    }

}
