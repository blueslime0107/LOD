using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card98 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(battle.myChar != card.player){return;}
        if(card.player.dice > enemy.dice && !enemy.dice.Equals(0)){
            int dis = card.player.dice - enemy.dice;
            card.player.AddDice(-dis);
            card.player.AddHealth(dis);
        }
    }
}
