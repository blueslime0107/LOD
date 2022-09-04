using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/2엑스칼리버", order = 2)]
public class Card2 : CardAbility
{

    public override void OnBattleWin(CardPack card,BattleCaculate battle)
    {
        //Actived();
        Active(card);
        battle.damage += 2;
    }

}
