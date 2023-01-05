using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/10해결사 상인", order = 10)]
public class Card10 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard)
    {
        if(card.player != player){return;}
        card.player.AddHealth(7);
        card.count = 4;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count -= 1;
        if(card.count <= 0){
            Damage newDam = new Damage();
            newDam.value = 4;
            card.player.DamagedBy(newDam,player);
            card.count = 4;
        }
    }


}

