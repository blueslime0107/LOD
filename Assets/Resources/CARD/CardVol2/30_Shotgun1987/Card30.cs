using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/30샷건", order = 30)]
public class Card30 : CardAbility
{

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        if(card.active){
            card.active = false;
            card.player.SetDice(2);
        }
        else{
            card.player.SetDice(card.player.dice + (int)Random.Range(2f,8f));
            card.active = true;
        }
    }
}
