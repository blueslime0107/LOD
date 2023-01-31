using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card82 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match){
        if(!card.active){return;}
        foreach(Player player in match.players){
            if(player == card.player){continue;}
            match.CardLog("Sleep",card,player);
            player.AddDice((int)-Mathf.Ceil(card.player.dice/2));
        }
        card.active = false;
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        if(match.GetDiceAverage() > 4){
            CardActivate(card,match);
        }
    }

}
