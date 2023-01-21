using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/19File_crashed", order = 23)]
public class Card23 : CardAbility
{
    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        match.CardLog(card);
        if(card.saved_card != null){
            match.DestroyCard(card.saved_card,card.player);
            card.saved_card = null;
        }
        List<CardAbility> random_caard = match.cards;
        random_caard.RemoveAll(x => x.card_id.Equals(23));
        card.saved_card = match.GiveCard(random_caard[(int)Random.Range(0f,(float)random_caard.Count)],card.player);
        card.saved_card.overCard = card.ability.overCard;
    }

}
