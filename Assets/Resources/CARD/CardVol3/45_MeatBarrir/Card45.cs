using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/45고기방패", order = 45)]
public class Card45 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        card.active = true;
        card.player.AddHealth(-2);
        match.CardLog(card);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active){return;}
        damage.value = 0;
        card.active = false;
        match.CardLog(card);
    }
}
