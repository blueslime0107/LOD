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
            foreach(Player players in match.players){
                if(players.dice.Equals(player.dice+count) && players != player){
                    count += 1;
                }

            }

                
        }
        player.SetDice(player.dice+count);

    }

}
