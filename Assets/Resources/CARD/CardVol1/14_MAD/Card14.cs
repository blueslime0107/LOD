using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card14 : CardAbility
{

    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(card.count >= 3){
            EffectPlayerSet(card.effect[0],card.player,battle.bm.transform);
            foreach(Player player in battle.bm.OpposeTeam(card.player.team).players){
                battle.bm.CardLog("Damage",card);
                player.NewDamagedByInt(card.count/3,card.player);
            }
            card.count %= 3;
        }
    }

    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match, Player attacker, Player defender)
    {
        if(defender.team.Equals(card.player.team)){
            if(defender.Equals(card.player)){
                card.count += damage.value;
            }
            else{
                card.count++;
            }
            
        }
        card.player.farAtt = card.count >= 3;
    }

    // public override void OnClashEnded(CardPack card, BattleCaculate battle)
    // {
    //     card.player.farAtt = card.count >= 3;
    // }

    // public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    // {
    //     Debug.Log(card.count);
    //     card.player.farAtt = card.count >= 3;
    // }

    // public override void AttackEffect(CardPack card, Player defender)
    // {
    //     if(card.player.dice_Indi.dice_list[0] == card.dice){
    //         EffectPlayerSet(card.effect[0],card.player,card.player.transform,-0.2f,-1f);
    //     }
    // }
}
