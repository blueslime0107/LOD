using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/속도카드", order = 165)]
public class SpdCard : CardAbility
{

    public override void ClashEnded(CardPack card)
    {
        if(card.count>0){
            card.player.SetDice((int)Random.Range(0,6)+1);
            card.count -= 1;
        }
    }

    
}
