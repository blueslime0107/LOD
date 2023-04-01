using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdCard : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        for(int i=0;i<pre_count;i++)
        {
            match.MakeNewDiceAndPutPlayer(card.player,(int)Random.Range(0,6)+1);
        }
    }

}
