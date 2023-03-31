using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/5", order = 5)]
public class Card68 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active || damage.value < 4){return;}
        match.CardLog("Block",card);
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,0);
        damage.setDamage(0); 
        card.active = false;
    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        if(card.count < 1 && !card.active){card.count++;}else{card.active = true;card.count=0;}
    }
}
