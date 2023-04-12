using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/속도카드", order = 164)]
public class NoAtkCard : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.player.dice_Indi.dice_list.Clear();
        card.player.dice_Indi.updateDice();
        if(card.count > 0){
            card.sub_count++;
            if(card.sub_count >= card.count){
                match.DestroyCard(card,card.player);
            }
        }
    }
}
