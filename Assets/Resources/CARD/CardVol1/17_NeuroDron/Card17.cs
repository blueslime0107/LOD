using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6정신지배드론", order = 17)]
public class Card17 : CardAbility
{

    public override void OnDamage(CardPack card, Player attackerbm, Damage damage, BattleManager match)
    {
        
        if(damage.value <= 2){
            damage.value = 0;
            match.CardLog(card);
            match.backColorEff.changeColor(0, 200, 255,200);
        }
    }
}
