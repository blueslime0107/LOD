using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card88 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.player.dice > card.count){
            card.sub_count += 1;
            if(card.sub_count > 2){
                card.active = true;
                match.MakeNewDiceAndPutPlayer(card.player,(int)Random.Range(0,6)+1);
                card.count = card.player.dice;
            }
            else{
                card.count = card.player.dice;
            }
        }
        else{
            card.active = false;
            card.sub_count = 0;
            card.count = card.player.dice;
        }
        
    }
}
