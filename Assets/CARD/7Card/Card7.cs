using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/7치명적인 일격", order = 7)]
public class Card7 : CardAbility
{

    public override void OnDamaging(CardPack card, Player defender, BattleManager match, int damage)
    {
        if(damage < 3){
            Active(card);
            damage *= 2;
            match.battleCaculate.damage = damage;
            //defender.getDamage = damage;
        }
        
    }
}
