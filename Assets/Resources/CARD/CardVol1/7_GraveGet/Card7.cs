using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card7 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player.team == match.OpposeTeam(card.player.team)){
            match.AddCardPoint(card.player.team);
            match.CardLog("GetCard",card,dead_player);
            EffectPlayerSet(card.effect[0],dead_player,dead_player.transform,0,-0.5f);
        }
        else{
            card.active = true;
            match.CardLog("AddDice",card,dead_player);
            EffectPlayerSet(card.effect[0],dead_player,dead_player.transform,0,-0.5f);
        }
        
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        match.MakeNewDiceAndPutPlayer(card.player,Random.Range(3,7));
        match.CardLog("AddDice",card);
        
    }
}
