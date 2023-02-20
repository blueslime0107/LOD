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
        Dice copyDice = new Dice();
        copyDice.dice_value = Random.Range(3,7);
        card.player.dice_Indi.put_subDice(copyDice);
        match.CardLog("AddDice",card);
        
    }
}
