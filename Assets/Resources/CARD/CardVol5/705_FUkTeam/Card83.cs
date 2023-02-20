using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card83 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        card.count ++;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.active = false;
        match.CardLog("PowerUp",card);
        card.player.AddDice(card.count);
        card.count = 0;
    }
    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(card.count > 4){
            CardActivate(card,match);
        }
    }

}
