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
        match.CardLog("Make Barrier",card);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active){return;}
        damage.value = 0;
        card.active = false;
        match.CardLog("Barrier Broken",card);
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(match.OpposeTeam(card.player.team).getDiceAverage() > 3 && card.player.health > 3){
            CardActivate(card,match);
        }
    }
}
