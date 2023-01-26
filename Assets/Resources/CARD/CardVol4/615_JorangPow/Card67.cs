using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card67 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.count ++;
        battle.bm.CardLog("Count",card);
        if(card.count >= 9){
            battle.bm.CardLog("Evolution",card);
            battle.bm.DestroyCard(card,card.player);
            battle.bm.GiveCard(linked_card[0],card.player);
        }
    }
}