using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/5", order = 5)]
public class Card68 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active || damage.value < 4){return;}
        match.CardLog(card);
        damage.setDamage(0); 
        card.count++;
        if(card.count >= 3){card.active = false;}
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }
}
