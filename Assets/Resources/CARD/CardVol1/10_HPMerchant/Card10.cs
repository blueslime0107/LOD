using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card10 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.AddHealth(7);
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,-1,-0.75f,true);
        card.count = 4;
        match.CardLog("Health",card);
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count -= 1;
        if(card.count <= 0){
            EffectPlayerSet(card.effect[1],card.player,card.player.transform,-1.7f,-0.1f,true);
            Damage newDam = new Damage();
            newDam.setDamage(4);
            card.player.DamagedBy(newDam,player);
            card.player.AddDice(3);
            card.count = 4;
            match.CardLog("Cost",card);
        }
    }


}

