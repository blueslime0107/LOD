using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6정신지배드론", order = 6)]
public class Card6 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker,  BattleManager match, int damage)
    {
        if(!card.card_enable){return;}
        if(damage <= 2){
            damage = 0;
            card.player.getDamage = damage;
        }
    }
}
