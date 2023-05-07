using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card97 : CardAbility
{
    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        if(defender.dice.Equals(0)){
            damage.adDamage(4);
        }
    }
}
