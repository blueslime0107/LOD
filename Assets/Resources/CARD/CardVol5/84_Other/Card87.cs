using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card87 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        damage.adDamage(card.count);
    }
}
