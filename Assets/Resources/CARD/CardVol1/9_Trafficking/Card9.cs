using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card9 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.dice >= 6){return;}
        card.player.AddHealth(-(int)Random.Range(0f,2f));
        card.player.SetDice(card.player.dice+1);
        match.CardLog("Dice",card);
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(card.player.health <= 2){return;}
        for(int i=0;i<2;i++){
            if(match.OpposeTeam(card.player.team).getDiceMaMin() > card.player.dice){
                CardActivate(card, match);
            }
        }
    }
}
