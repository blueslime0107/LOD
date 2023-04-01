using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card14 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        if(damage.value <= 0){return;}
        card.dice = match.MakeNewDice(1);
        foreach(Player player in attacker.team.players){
            match.battleCaculate.MakeNewEventBattle(card.player,player,card.dice,0);
            
        }
        
    }

    // public override void AttackEffect(CardPack card, Player defender)
    // {
    //     if(card.player.dice_Indi.dice_list[0] == card.dice){
    //         EffectPlayerSet(card.effect[0],card.player,card.player.transform,-0.2f,-1f);
    //     }
    // }
}
