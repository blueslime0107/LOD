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
        battle.AddDamage(2);
        
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        card.effect[0].transform.position = defender.gameObject.transform.position;
        card.effect[0].SetActive(true); 
    }

}
