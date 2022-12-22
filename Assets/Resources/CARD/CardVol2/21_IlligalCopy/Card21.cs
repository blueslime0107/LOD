using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/20프로불펌러", order = 21)]
public class Card21 : CardAbility
{

    public override void WhenCardGet(CardPack card,BattleManager match, Player player)
    {
        card.saved_card = match.GiveCard(linked_card[0],player);
    }
    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card,BattleManager match)
    {

        card.saved_card.ability = selected_card.ability;
        card.saved_card.illust = selected_card.illust;
        card.saved_card.name = selected_card.name;
        card.saved_card.ability_message = selected_card.ability_message;
        card.saved_card.message = selected_card.message;

    }

}