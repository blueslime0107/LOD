using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/16글록17", order = 16)]
public class Card16 : CardAbility
{
    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        Dice copyDice = dice;
        copyDice.dice_value = 1;
        card.player.dice_Indi.put_subDice(copyDice);
    }
    

    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.card_activating = false;
    }
}
