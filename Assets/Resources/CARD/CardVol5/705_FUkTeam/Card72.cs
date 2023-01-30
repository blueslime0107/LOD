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
            match.sdm.Play("Pumble");
            card.player.SetDice(0);

        }
        if(newValue > 3 && newValue <= 10){
            match.CardLog("Failed",card);
            match.sdm.Play("Failed");
            card.player.AddDice(-2);
        }
        if(newValue > 10 && newValue <= 17){
            match.CardLog("Success",card);
            match.sdm.Play("Success");
            card.player.AddDice(2);
        }
        if(newValue > 17){
            match.CardLog("Critical",card);
            match.sdm.Play("HappySuccess");
            foreach(Player player in match.players){
                if(player == card.player){continue;}
                player.SetDice(0);
            }
        }
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.player.team.battleAI.active){return;}
        card.count ++;
        if(card.count > 1){
            CardActivate(card, match);
            card.count = 0;
        }
    }
}
