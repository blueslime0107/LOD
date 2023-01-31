using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card80 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.count += 1;
        battle.damage.adDamage(card.count);
        battle.bm.CardLog("PowerUp",card);
        if(card.count >= 6){
            battle.bm.GiveCard(linked_card[0],enemy);
            battle.bm.DestroyCard(card,card.player);
        }
    }

    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(battle.damage.value >= card.count){
            card.count --;
        }
    }
}
