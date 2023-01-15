using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card195 : CardAbility
{
    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        damage.value = 0;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.count.Equals(10)){
            card.player.cards.Remove(card);
        }
        card.count = 10;
    }

}
