using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card67 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.count ++;
        battle.bm.CardLog("Count",card);
        
        if(card.count >= 3){
            battle.bm.CardLog("Evolution",card);
            EffectPlayerSet(card.effect[1],card.player,card.player.transform,1,0);
            battle.bm.DestroyCard(card,card.player);
            battle.bm.GiveCard(linked_card[0],card.player);
        }
        else{
            EffectPlayerSet(card.effect[0],card.player,card.player.transform,1,0);
        }
    }
}