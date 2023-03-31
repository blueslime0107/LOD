using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card62 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        Active(card);
        battle.AddDamage(1);
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,1.75f,-1,true);

        card.count += 1;
        if(card.count >= 2){
            card.active = true;
        }
    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        match.CardLog("Destroyed",card);
        match.DestroyCard(card,card.player);
    }
}
