using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/16글록17", order = 16)]
public class Card16 : CardAbility
{
    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        Dice copyDice = dice;
        copyDice.dice_value = 1;
        card.player.dice_Indi.put_subDice(copyDice);
        card.dice = copyDice;
    }
    

    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = false;
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        if(card.player.dice_Indi.dice_list[0].Equals(card.dice)){
            if(!card.player.farAtt)
                card.effect[0].SetActive(true);
        }
        
    }
}
