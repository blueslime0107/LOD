using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/11명현현상", order = 28)]
public class Card28 : CardAbility
{
    
    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        player.AddHealth((int)Random.Range(1,3));
        if(player.max_health.Equals(player.health)){
            card.count += 1;
        }
        card.effect[0].SetActive(true);
        if(card.count >= 3){
            player.YouAreDead();
        }
    }
}
