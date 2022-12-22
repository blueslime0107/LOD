using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14네잎클로버", order = 12)]
public class Card12 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        foreach(Player player in match.players){
            if(player.dice >= 5){
                player.SetDice(0);
            }
            
        }
    }
}
