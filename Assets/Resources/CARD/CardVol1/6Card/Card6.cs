using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6정신지배드론", order = 6)]
public class Card6 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker,  BattleManager match, int damage)
    {
        
        if(match.battleCaculate.damage <= 2){
            Active(card);
            match.battleCaculate.damage = 0;
        }
    }
}
