using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/16글록17", order = 16)]
public class Card16 : CardAbility
{

    public override void BattleEnded(CardPack card)
    {
        if(!card.card_activating){
            card.player.SetDice(1);
            card.card_activating = true;
        }
    }

    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        card.card_activating = false;
    }
}
