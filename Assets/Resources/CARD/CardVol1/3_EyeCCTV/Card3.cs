using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/3눈알CCTV", order = 3)]
public class Card3 : CardAbility
{

    public override void WhoEverDamage(CardPack card, Damage damage)
    {
        if(damage.value >= 3){
            card.player.AddHealth(2);
        }
    }
}
