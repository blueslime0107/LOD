using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/25미믹", order = 25)]
public class Card25 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player)
    {
        if(card.player.tag.Equals(player.tag)){return;}
        int randint = 0;
        randint = (int)Random.Range(1f,11f);
        if(randint < card.count){
            player.AddHealth(-Mathf.CeilToInt(card.count / 2));
            card.count = -1;
            match.backColorEff.gameObject.SetActive(true);
            match.backColorEff.changeColor(255, 162, 0,255);
        }
        card.count += 1;

    }
}