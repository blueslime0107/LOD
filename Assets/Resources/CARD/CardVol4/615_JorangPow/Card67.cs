using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card67 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.count ++;
        if(card.count >= 3){
            battle.bm.CardLog(card);
            battle.bm.DestroyCard(card,card.player);
            battle.bm.GiveCard(linked_card[0],card.player);
        }
    }
}