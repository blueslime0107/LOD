using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card63 : CardAbility
{
    public override void OnClashLose(CardPack card, BattleCaculate battle)
    {
        battle.bm.CardLog(card);
        battle.damage.adDamage(-2);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        match.CardLog(card,card.player);
        damage.adDamage(1);
    }
}