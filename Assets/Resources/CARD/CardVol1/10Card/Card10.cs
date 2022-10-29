using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/10D6", order = 10)]
public class Card10 : CardAbility
{
    public override void ImmediCardDraw(CardPack card, BattleManager match, Player player)
    {
        if(match.left_turn){
            match.card_left_draw += 1;
            match.left_d6 = true;
            match.left_d6_Count = 3;
        }
        else{
            match.card_right_draw += 1;
            match.right_d6 = true;
            match.right_d6_Count = 3;
        }
    }

}
