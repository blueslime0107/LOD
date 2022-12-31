using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/22복사로봇", order = 22)]
public class Card22 : CardAbility
{

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        
        int bigNum = 0;
        if(card.active){
            card.active = false;
            return;
        }
        foreach(Player play in (player.tag.Equals("PlayerTeam1")) ? match.right_players : match.left_players){
            if(play.dice > bigNum){
                bigNum = play.dice;
            }
        }
        Debug.Log(bigNum);
        card.player.SetDice(bigNum);
        if(bigNum >= 5){
            card.active = true;
        }
    }
}
