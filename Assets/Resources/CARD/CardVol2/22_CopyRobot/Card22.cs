using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/22복사로봇", order = 22)]
public class Card22 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        
        int bigNum = 0;
        if(!card.active){
            card.active = true;
            return;
        }
        foreach(Player play in match.OpposeTeam(card.player.team).players){
            if(play.dice > bigNum){
                bigNum = play.dice;
            }
        }
        card.player.SetDice(bigNum);
        if(bigNum >= 5){
            card.active = false;
        }
        match.CardLog("Copyed",card);
    }
}
