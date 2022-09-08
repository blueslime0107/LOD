using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/19File_crashed", order = 19)]
public class Card19 : CardAbility
{

    public override void ImmediCardDraw(CardPack card,BattleManager match, Player player)
    {
        card.saved_card = match.GiveCard(linked_card[0],player);
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        List<CardAbility> random_caard = match.cards;
        random_caard.RemoveAll(x => x.card_id.Equals(19));
        card.saved_card.ability = random_caard[(int)Random.Range(0f,(float)random_caard.Count)];
        Debug.Log(card.saved_card.ability.name + "발동!");
    }
}
