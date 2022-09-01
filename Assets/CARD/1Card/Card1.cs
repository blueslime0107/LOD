using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/1의식실패", order = 1)]
public class Card1 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.card_enable){return;}
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, BattleManager match)
    {
        Debug.Log("card_selected!");
        Debug.Log(card.selected_card.ability.name);
        card.selected_card.card_enable = !card.selected_card.card_enable;

    }
}
