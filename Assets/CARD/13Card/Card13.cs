using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/13눈알CCTV", order = 13)]
public class Card13 : CardAbility
{

    public override void WhoEverDamage(CardPack card, int damage)
    {
        if(damage >= 3){
            card.player.AddHealth(2);
        }
    }
}
