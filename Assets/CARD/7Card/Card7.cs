using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/7치명적인 일격", order = 7)]
public class Card7 : CardAbility
{

    public override void OnDamaging(CardPack card, Player defender, BattleManager match, int damage)
    {
        if(!card.card_enable){return;}
        if(damage < 3){
            //this.card_active = true;
            damage *= 2;
            defender.getDamage = damage;
        }
        
    }
}
