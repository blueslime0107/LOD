using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card63 : CardAbility
{
    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog("Block",card);
            EffectPlayerSet(card.effect[0],card.player,card.player.transform,1,-0.7f,true);
        battle.damage.adDamage(-2);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        match.CardLog("Broken",card,card.player);
        EffectPlayerSet(card.effect[1],card.player,card.player.transform,1,-0.6f,true);

        match.DestroyCard(card,card.player);
    }
}