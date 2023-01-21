using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/25미믹", order = 25)]
public class Card25 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard)
    {
        if(card.player.tag.Equals(player.tag)){return;}
        int randint = 0;
        randint = (int)Random.Range(1f,11f);
        if(randint < card.count){
            player.AddHealth(-Mathf.CeilToInt(card.count / 2));
            getCard.player.cards.Remove(getCard);
            card.count = -1;
            match.backColorEff.changeColor(255, 162, 0,255);
            match.CardLog(card,player);
        }
        card.count += 1;

    }
}
