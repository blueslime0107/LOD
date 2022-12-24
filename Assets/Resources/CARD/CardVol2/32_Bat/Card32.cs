using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/32샷건", order = 32)]
public class Card32 : CardAbility
{

    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.player.dice != enemy.dice){return;}
        card.player.AddDice(3);
    }

}
