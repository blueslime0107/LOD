using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/속도카드", order = 165)]
public class SpdCard : CardAbility
{

    public override void BattleEnded(CardPack card)
    {
        if(card.gague>0){
            card.player.SetDice((int)Random.Range(0,6)+1);
            card.gague -= 1;
        }
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.gague = card.max_gague;
    }
    
}
