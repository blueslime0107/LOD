using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/34아이기스", order = 34)]
public class Card34 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.player.dice != 0){return;}
        damage.value -= 3;
        if(damage.value <= 0){damage.value = 0;}
    }
}
