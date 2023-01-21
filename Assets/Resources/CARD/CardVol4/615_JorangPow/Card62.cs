using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card62 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle)
    {
        battle.bm.CardLog(card);
        card.player.AddDice(1);
        Active(card);
        card.count += 1;
        if(card.count >= 2){
            card.active = true;
        }
    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        match.CardLog(card);
        match.DestroyCard(card,card.player);
    }
}
