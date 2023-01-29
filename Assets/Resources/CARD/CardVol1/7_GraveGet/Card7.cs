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
        }
        else{
            card.active = true;
            match.CardLog("AddDice",card,dead_player);
        }
        
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.count = (int)Random.Range(2f,6f);
        
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active || card.count.Equals(0)){return;}
        int dice;
        dice = card.player.dice;
        card.player.SetDice(card.count);
        card.count = dice;
        match.CardLog("Active",card);
        
    }

    public override void OnClashEnded(CardPack card, BattleCaculate battle)
    {
        if(!card.active || card.count.Equals(0)){return;}
        int dice = card.player.dice;
        card.player.SetDice(card.count);
        card.count = dice;
        battle.bm.CardLog("Dice",card);
    }
}
