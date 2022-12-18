using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/9인신매매", order = 9)]
public class Card_Trafficking : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.dice >= 6){return;}
        card.player.AddHealth(-(int)Random.Range(0f,2f));
        card.player.SetDice(card.player.dice+1);
    }
}