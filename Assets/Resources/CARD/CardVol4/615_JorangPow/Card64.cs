using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card64 : CardAbility
{
    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = !card.active;
    }
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        foreach(Player play in player.team.players ){
            if(play.dice <= 3){
                match.CardLog(card,play);
                play.AddDice(1);
            }
        }
    }
}