using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Card84 : CardAbility
{
    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        if(card.active){return;}
        card.count -= 1;
        if(card.count <= 0){
            card.active = true;
        }
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(!card.active){return;}
        if(enemy.cards.Count > 0){
            battle.bm.CardLog("MimicDestroy",card,enemy);
            battle.bm.backColorEff.changeColor(255, 162, 0,255);
            battle.bm.DestroyCard(enemy.cards[0],enemy);
            card.player.AddHealth(3);
        }
        else{
            battle.bm.backColorEff.changeColor(255, 162, 0,255);
            battle.bm.CardLog("MimicPower",card);
            card.player.AddDice(3);
        }
        card.active = false;
        card.count = 2;
    }
}
