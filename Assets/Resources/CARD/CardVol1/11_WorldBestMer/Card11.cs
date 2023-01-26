using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/11이세상 최고의 연금술", order =11)]
public class Card11 : CardAbility
{
    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match,Player attacker,Player defender)
    {
        if(card.count >= 15){card.count = 15; return;}
        match.CardLog("Count",card);
        card.count += damage.value;
        if(card.count >= 15){card.count = 15;}

    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.count < 15){return;}
        match.CardLog("GetCard",card);
        card.count = 0;
        card.player.team.carddraw += 1;
    }
}
