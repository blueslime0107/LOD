using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card74 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.card_reg.Count == 0){
        List<CardAbility> origin_cards = new List<CardAbility>(linked_card);
        int origin_count = origin_cards.Count;
        for(int i = 0; i<origin_count;i++){
            int rand_card = Random.Range(0,origin_cards.Count);
            card.card_reg.Add(origin_cards[rand_card]);
            origin_cards.RemoveAt(rand_card);

        }

        }
        match.CardLog("GetScroll",card);
        match.GiveCard(card.card_reg[0],card.player);
        card.card_reg.RemoveAt(0);
    }
}
