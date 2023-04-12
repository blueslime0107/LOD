using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card89 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        foreach(DiceProperty diceProperty in card.player.dice_Indi.dice_list){
            diceProperty.value = 1;
        }
        card.player.dice_Indi.updateDice();
    }
}
