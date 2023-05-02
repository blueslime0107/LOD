using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card88 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.active = true;
        match.MakeNewDiceAndPutPlayer(card.player,(int)Random.Range(0,6)+1);
    }
}
