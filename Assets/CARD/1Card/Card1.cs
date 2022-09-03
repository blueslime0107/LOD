using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/1의식실패", order = 1)]
public class Card1 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, BattleManager match)
    {
        Debug.Log(card.saved_card);
        try{
            card.saved_card.ability = card.saved_ability;
            Debug.Log("released!");
            Debug.Log(card.saved_card.ability.name);
        }
        catch{
            Debug.Log("Error!");
        }
            
        
        Debug.Log("card_selected!");
        Debug.Log(card.selected_card.ability.name);
        if(card.selected_card.ability.Equals(this)){
            return;
        }
        card.saved_ability = card.selected_card.ability;
        card.selected_card.ability = match.null_card;
        card.saved_card = card.selected_card;
        

    }
}
