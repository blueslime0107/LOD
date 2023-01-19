using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card33 : CardAbility
{

    public override void OnClashWin(CardPack card,BattleCaculate battle)
    {
        Active(card);
        battle.AddDamage(2);
        battle.bm.CardLog(card);
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        card.effect[0].transform.position = defender.gameObject.transform.position;
        card.effect[0].SetActive(true); 
    }

}
