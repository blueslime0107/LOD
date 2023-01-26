using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card49 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player plaer, BattleManager match)
    {
        if(!card.active){
            if(card.count == 1){
                card.count = 0;
                card.active = true;
            }
            else{
            foreach(Player player in card.player.team.players){
            player.AddDice(-3);
            }
            card.count = 1;
            return;
            }
        }

        foreach(Player player in card.player.team.players){
            player.AddDice(1);
            if(player.died){
                card.player.AddDice(1);
            }
        }
        match.CardLog("PowerUp",card);
    }

    public override void OnClashLose(CardPack card, BattleCaculate battle)
    {
        card.active = false;
        battle.bm.backColorEff.changeColor(255,255,255,200);
        battle.bm.CardLog("Lose",card);
    }
}
