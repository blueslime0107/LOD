using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card83 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count ++;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {

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
