using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card77 : CardAbility
{
    
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        int newValue = (int)Random.Range(1f,21f);
        if(newValue <= 3){
            battle.bm.CardLog("Pumble",card);
            battle.bm.sdm.Play("Pumble");
            if(enemy.cards.Count <= 0){return;}
            battle.bm.DestroyCard(enemy.cards[0],enemy);

        }
        if(newValue > 3 && newValue <= 10){
            battle.bm.CardLog("Failed",card);
            battle.bm.sdm.Play("Failed");
            enemy.AddBreak(-3);

        }
        if(newValue > 10 && newValue <= 17){
            battle.bm.CardLog("Success",card);
            battle.bm.sdm.Play("Success");
            enemy.AddBreak(3);
        }
        if(newValue > 17){
            battle.bm.CardLog("Critical",card);
            battle.bm.sdm.Play("HappySuccess");
            battle.bm.AddCardPoint(enemy.team);
        }
        battle.bm.DestroyCard(card,card.player);
    }
}
