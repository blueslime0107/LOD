using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card5 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        if(card.player.dice <= 0){
            int reduce = 0;
            reduce = (int)Mathf.Floor(damage.value / 2);
            damage.value -= reduce;
            Active(card);
            attacker.DamagedByInt(reduce,card.player,damage,card);
            
            
        }
        else{
            damage.value -= 1;
            Active(card);
            attacker.DamagedByInt(1,card.player,damage,card);
            

        }


    }
}
