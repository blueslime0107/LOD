using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card71 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog("PowerUp",card);
        battle.damage.adDamage(2);
        card.saved_player = battle.eneChar;
    }

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player == card.saved_player){
            match.CardLog("Broken",card);
            match.DestroyCard(card,card.player);
        }
    }

    public override void OnClashEnded(CardPack card, BattleCaculate battle)
    {
        card.saved_player = null;
    }

}
