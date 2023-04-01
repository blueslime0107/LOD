using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card65 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        match.MakeNewDiceAndPutPlayer(card.player,Random.Range(1,7));
        match.CardLog("AddDice",card);
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-1,true);
        card.active = false;
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(card.active){
            CardActivate(card,match);
        }
    }
}