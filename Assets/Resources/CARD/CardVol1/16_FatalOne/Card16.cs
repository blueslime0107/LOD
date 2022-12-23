using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/7치명적인 일격", order = 16)]
public class Card16 : CardAbility
{

    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        if(damage.value < 3){
            Active(card);
            damage.value *= 2;
            //defender.getDamage = damage;
        }
        
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        if(!card.card_battleActive){return;}
        card.effect[0].transform.position = defender.gameObject.transform.position;
        card.effect[0].SetActive(true); 
    }

    // public override void AttackEffect(CardPack card, Player defender)
    // {
    //     if(!card.card_lateActive){
    //         return;
    //     }
    //     card.effect[0].transform.position = defender.gameObject.transform.position;
    //     card.effect[0].SetActive(true);
    //     card.card_lateActive = false;
    // }
}
