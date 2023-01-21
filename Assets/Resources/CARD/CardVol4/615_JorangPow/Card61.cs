using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card61 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {
        battle.bm.CardLog(card);
        battle.ones_power = 2;
    }
}
