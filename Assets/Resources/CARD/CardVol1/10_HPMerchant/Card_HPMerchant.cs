using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/10해결사 상인", order = 10)]
public class Card_HPMerchant : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player)
    {
        if(card.player != player){return;}
        card.player.AddHealth(7);
        card.count = 4;
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        card.count -= 1;
        if(card.count <= 0){
            card.player.DamagedBy(4,player);
            card.count = 4;
        }
    }


}

