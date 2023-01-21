using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card66 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        match.CardLog(card);
        card.player.AddHealth(2);
        card.active = true;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        match.CardLog(card);
        card.player.AddDice(1);
        card.active = false;
    }
}