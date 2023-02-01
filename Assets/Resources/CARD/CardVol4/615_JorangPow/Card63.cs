using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card63 : CardAbility
{
    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog("Block",card);
        battle.damage.adDamage(-2);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        match.CardLog("Broken",card,card.player);
        match.DestroyCard(card,card.player);
    }
}