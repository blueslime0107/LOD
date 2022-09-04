using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/15솎아내기", order = 15)]
public class Card15 : CardAbility
{
    
    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        player.AddHealth(1);
        card.effect[0].SetActive(true);
        //EffectPlayerSet(card.effect[0],player,player.transform,1,-0.5f);
    }
}
