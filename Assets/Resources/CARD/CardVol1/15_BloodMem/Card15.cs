using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card15 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        Active(card);
        match.CardLog(card,attacker);
        attacker.DamagedByInt((int)Mathf.Ceil(damage.value*0.5f),card.player,damage,card);
        match.backColorEff.changeColor(255,0,0,255);
        
    }
}
