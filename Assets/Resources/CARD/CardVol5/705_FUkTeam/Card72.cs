using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card72 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        int newValue = (int)Random.Range(1f,21f);
        if(newValue <= 3){
            match.CardLog("Pumble",card);
            card.player.SetDice(0);

        }
        if(newValue > 3 && newValue <= 10){
            match.CardLog("Failed",card);
            card.player.AddDice(-2);
        }
        if(newValue > 10 && newValue <= 17){
            match.CardLog("Success",card);
            card.player.AddDice(2);
        }
        if(newValue > 17){
            match.CardLog("Critical",card);
            foreach(Player player in match.players){
                if(player == card.player){continue;}
                player.SetDice(0);
            }
        }
    }
}
