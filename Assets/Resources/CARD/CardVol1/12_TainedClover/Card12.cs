using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14네잎클로버", order = 12)]
public class Card12 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.dice > 2 || card.count <= 0){return;}
        card.diceLink.positionCount = 0;
        foreach(Player player in (card.count.Equals(1)) ? match.players : match.OpposeTeam(card.player.team).players){
            if(player.dice >= 5){
                player.SetDice(0);
                card.diceLink.positionCount += 2;
                card.diceLink.SetPosition(card.diceLink.positionCount-2,player.dice_Indi.gameObject.transform.position);
                card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
                match.CardLog("BreakDice",card,player);
            }
            
        }
        card.diceLink.gameObject.SetActive(true);
        card.active = true;
        card.count -= 1;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.active = false;
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        int newint = 0;
        foreach(Player player in match.OpposeTeam(card.player.team).players){
            if(player.dice >= 5){
                newint ++;
            }
        }
        if(newint >= 2){
            CardActivate(card,match);
        }
    }
}
