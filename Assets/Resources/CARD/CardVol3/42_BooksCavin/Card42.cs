using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card42 : CardAbility
{
    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player.cards.Count <= 0 || dead_player.Equals(card.player)){return;}
        match.GiveCardPack(dead_player.cards[0],card.player);
        match.CardLog("GetCard",card,dead_player);
    }
}
