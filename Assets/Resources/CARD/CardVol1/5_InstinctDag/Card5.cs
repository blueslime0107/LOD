using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/5본능의 단검", order = 5)]
public class Card5 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(damage.origin == this){return;}
        if(card.player.dice <= 0){
            int reduce = 0;
            reduce = (int)Mathf.Floor(damage.value / 2);
            damage.value -= reduce;
            attacker.DamagedByInt(reduce,card.player,damage,card);
        }
        else{
            damage.value -= 1;
            attacker.DamagedByInt(1,card.player,damage,card);

        }


    }
}
