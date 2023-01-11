using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/11이세상 최고의 연금술", order =11)]
public class Card11 : CardAbility
{
    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match)
    {
        card.count += damage.value;
        if(card.count >= 15){card.count = 15;}

    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.count < 15){return;}
        card.count = 0;
        if(card.player.tag.Equals("PlayerTeam1")){match.card_left_draw += 1;}else{match.card_right_draw += 1;}
    }
}
