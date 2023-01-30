using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card73 : CardAbility
{
    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.card_battleActive){return;}
        card.saved_player = battle.bm.players[Random.Range(0,battle.bm.players.Count)];
        Active(card);
        battle.bm.CardLog("What?",card,card.saved_player);
        card.saved_player.NewDamagedByInt(battle.damage.value,card.player);
    }
}
