using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/42북스캐빈저", order = 42)]
public class Card42 : CardAbility
{
    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player.cards.Count <= 0){return;}
        match.GiveCardPack(dead_player.cards[0],card.player);
    }
}
