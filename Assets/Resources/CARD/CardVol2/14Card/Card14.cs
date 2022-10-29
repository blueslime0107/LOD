using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14네잎클로버", order = 14)]
public class Card14 : CardAbility
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
