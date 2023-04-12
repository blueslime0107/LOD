using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/16글록17", order = 29)]
public class Card29 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        match.MakeNewDiceAndPutPlayer(card.player,1);
        match.CardLog("AddDice",card);
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        if(card.player.dice_Indi.dice_list.Count <= 0){return;}
        if(card.player.dice_Indi.dice_list[0].Equals(card.dice)){
            if(!card.player.farAtt)
                card.effect[0].gameObject.SetActive(true);
        }
        
    }
}
