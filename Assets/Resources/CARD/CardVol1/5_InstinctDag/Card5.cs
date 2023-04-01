using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card5 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.active){return;}
        if(card.player.dice <= 0){
            card.saved_int = (int)Mathf.Floor(damage.value / 2);
            damage.adDamage(-card.saved_int);
            card.active = true;
            card.saved_player = attacker;
            match.CardLog("Defenceless",card,attacker);
            
        }
        else{
            damage.adDamage(-1);
            card.saved_int = 1;
            card.active = true;
            card.saved_player = attacker;
            match.CardLog("Defenceless",card,attacker);
        }


    }

    public override void OnClashEnded(CardPack card, BattleCaculate battle)
    {
        if(!card.active){return;}
        card.active = false;
        card.dice = battle.bm.MakeNewDice(card.saved_int);
        battle.MakeNewEventBattle(card.player,card.saved_player,card.dice,0);
    }

    public override void AttackEffect(CardPack card, Player defender)
    {
        if(card.player.dice_Indi.dice_list[0] == card.dice){
            EffectPlayerSet(card.effect[0],defender,defender.transform,-0.2f,-1f);
        }
    }
}
