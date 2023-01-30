using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card76 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        int newValue = (int)Random.Range(1f,21f);
        if(newValue <= 3){
            battle.bm.CardLog("Pumble",card);
            battle.bm.sdm.Play("Pumble");
            card.player.SetDice(0);

        }
        if(newValue > 3 && newValue <= 10){
            battle.bm.CardLog("Failed",card);
            battle.bm.sdm.Play("Failed");
            card.player.AddDice(-3);
        }
        if(newValue > 10 && newValue <= 17){
            battle.bm.CardLog("Success",card);
            battle.bm.sdm.Play("Success");
            card.player.AddDice(3);
        }
        if(newValue > 17){
            battle.bm.CardLog("Critical",card);
            battle.bm.sdm.Play("HappySuccess");
            card.player.AddDice(card.player.dice);
        }
        battle.bm.DestroyCard(card,card.player);
    }
}
