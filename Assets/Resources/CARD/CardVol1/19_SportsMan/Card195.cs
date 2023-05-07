using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card195 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.player.dice <= 0){
            battle.bm.CardLog("Dice1",card);
            battle.bm.MakeNewDiceAndPutPlayer(card.player,1,true);
        }
    }

}
