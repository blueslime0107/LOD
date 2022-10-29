using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/15솎아내기", order = 15)]
public class Card15 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.dice >= 1){
            card.player.AddDice(-1);
            card.player.AddHealth(1);
        }
        
    }
}
