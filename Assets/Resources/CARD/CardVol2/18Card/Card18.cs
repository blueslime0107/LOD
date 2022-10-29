using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/18핏빛기억", order = 18)]
public class Card18 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, BattleManager match, int damage)
    {
        attacker.Damage((int)Mathf.Ceil(damage*0.5f),card.player);
    }
}
