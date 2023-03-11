using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_IllKillU : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        match.CardLog("Damage",card);
        card.count += 2;
        player.NewDamagedByInt(card.count,player);
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-1.2f);
    }
}