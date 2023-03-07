using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card5 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        if(card.player.dice <= 0){
            int reduce = 0;
            reduce = (int)Mathf.Floor(damage.value / 2);
            damage.adDamage(-reduce);
            Active(card);
            match.CardLog("Damage",card,attacker);
            EffectPlayerSet(card.effect[0],attacker,attacker.transform,-0.2f,-1f);
            attacker.DamagedByInt(reduce,card.player,damage,card);
            
        }
        else{
            damage.adDamage(-1);
            Active(card);
            match.CardLog("Defenceless",card,attacker);
            EffectPlayerSet(card.effect[0],attacker,attacker.transform,-0.2f,-1f);

            attacker.DamagedByInt(1,card.player,damage,card);
            
        }


    }
}
