using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/18핏빛기억", order = 15)]
public class Card15 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        attacker.DamagedByInt((int)Mathf.Ceil(damage.value*0.5f),card.player);
    }
}