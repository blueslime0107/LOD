using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card4 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-2);
        card.effect[0].gameObject.SetActive(true);
    }


    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player == battle.myChar && card.player.dice > 0 && enemy.dice >= card.player.dice){
        card.player.SetDice(1);
        enemy.SetDice(0);
        battle.bm.CardLog("Damage",card,battle.eneChar);

        }

        

    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        EffectPlayerSet(card.effect[1],defender,defender.transform,-0.4f,-0.8f);
    }
}
