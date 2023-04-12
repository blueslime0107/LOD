using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/11이세상 최고의 연금술", order =11)]
public class Card11 : CardAbility
{
    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match,Player attacker,Player defender)
    {
        match.CardLog("Count",card);
        card.count++;
        if(card.count >= 6 + card.sub_count){
            match.CardLog("GetCard",card);
            EffectPlayerSet(card.effect[1],card.player,match.battleCaculate.battleDice.transform,0,-3);
        card.count = 0;
        match.AddCardPoint(card.player.team);
        card.sub_count += 1;
        }
        else{
            EffectPlayerSet(card.effect[0],card.player,match.battleCaculate.battleDice.transform,0,-3);
        }

    }
}
