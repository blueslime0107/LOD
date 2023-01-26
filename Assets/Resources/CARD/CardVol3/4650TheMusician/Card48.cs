using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card48 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.active)
        {card.count += 1;
        foreach(Player players in match.OpposeTeam(card.player.team).players){
            players.AddHealth((card.count >= 2) ? 2:card.count);
            players.AddDice(-card.count);
            match.CardLog("Health",card,players);
        }
        card.active = true;
        }
        else{
            card.active = false;
        }

        

    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.player.dice >= 1){return;}
        card.count = 0;
        card.active = true;
        match.CardLog("Damaged",card);
        match.backColorEff.changeColor(94, 94, 94,210);
    }

}
