using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card38 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
        card.count = Random.Range(1,6);
        
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count -= 1;
        if(card.count > 0){return;}
        card.active = false;
        card.player.NewDamagedByInt(card.player.max_health/2,card.player);
        match.GiveCard(match.cards[Random.Range(0,match.cards.Count)],card.player);
        match.DestroyCard(card,card.player);

    }

    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog("Bug<Dice-1>",card);
        card.player.AddDice(-1);
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {

        battle.bm.CardLog("Give Bug",card);
        battle.bm.GiveCardPack(card,enemy);
    }
}
