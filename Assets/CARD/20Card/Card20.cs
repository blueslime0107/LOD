using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/20프로불펌러", order = 20)]
public class Card20 : CardAbility
{
    
    public override void MatchStarted(CardPack card, Player player, BattleManager match)
    {
        player.AddHealth(1);
        card.effect[0].SetActive(true);
        //EffectPlayerSet(card.effect[0],player,player.transform,1,-0.5f);
    }
}
