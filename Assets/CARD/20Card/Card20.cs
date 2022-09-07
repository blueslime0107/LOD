using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/20프로불펌러", order = 20)]
public class Card20 : CardAbility
{

    public override void ImmediCardDraw(CardPack card,BattleManager match, Player player)
    {
        card.saved_card = match.GiveCard(linked_card[0],player);
    }
    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, BattleManager match)
    {

        card.saved_card.ability = card.selected_card.ability;
        card.saved_card.illust = card.selected_card.illust;
        card.saved_card.name = card.selected_card.name;
        card.saved_card.ability_message = card.selected_card.ability_message;
        card.saved_card.message = card.selected_card.message;

    }

}
