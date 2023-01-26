using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/15솎아내기", order = 37)]
public class Card37 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.dice > 1){
            card.player.AddDice(-1);
            
            int rand = (int)Random.Range(0f,101f);
            if(rand <= card.count){
                card.player.AddHealth(1);
                card.count -= 5;
                match.CardLog("Health",card);
            }
        }
        
    }
}
