using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/11명현현상", order = 28)]
public class Card28 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match){
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,-1.74f,-0.24f,true);
    }
    
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        player.AddHealth((int)Random.Range(1,3));
        match.CardLog("Health",card);
        if(player.max_health.Equals(player.health)){
            match.CardLog("Accident",card);
            player.AddHealth(-card.player.max_health/2);
            

        }
    }
}
