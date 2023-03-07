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
        if(card.count >= 15){
            match.CardLog("GetCard",card);
            EffectPlayerSet(card.effect[1],card.player,match.battleCaculate.battleDice.transform,0,-3);
        card.count = 0;
        match.AddCardPoint(card.player.team);
        }
        else{
            EffectPlayerSet(card.effect[0],card.player,match.battleCaculate.battleDice.transform,0,-3);
        }

    }
}
