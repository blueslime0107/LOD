using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/4숟가락 살인마", order = 4)]
public class Card4 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player.dice > enemy.dice){return;}

        card.player.SetDice(1);
        enemy.SetDice(0);
        battle.bm.CardLog("Damage",card,battle.eneChar);
        

    }
}
