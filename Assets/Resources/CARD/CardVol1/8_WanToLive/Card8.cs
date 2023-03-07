using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card8 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player || card.count >= 2){return;}
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-1f);
        card.player.AddHealth(1);
        card.active = true;
        match.CardLog("Revival",card);
        
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        player.SetDice(player.dice + 4);
        match.CardLog("PowerUp",card);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active){return;}
        Active(card);
        damage.value = 0;
        match.CardLog("NoDamage",card);

    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.count += 1;
        if(card.count >= 2)
           { card.active = false;
            card.effect[0].SetActive(false);}

    }
}
