using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/22복사로봇", order = 22)]
public class Card22 : CardAbility
{

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        
        int bigNum = 0;
        if(card.active){
            card.active = false;
            return;
        }
        foreach(Player play in (player.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            if(play.health > bigNum){
                bigNum = play.dice;
            }
        }
        card.player.SetDice(bigNum);
        if(bigNum >= 5){
            card.active = true;
        }
    }
}
