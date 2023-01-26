using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/20프로불펌러", order = 21)]
public class Card21 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.active = false;
    }

    public override void CardSelected(CardPack card, CardPack selected_card,BattleManager match)
    {
        if(card.active){return;}
        if(card.saved_card != null){
            match.DestroyCard(card.saved_card,card.player);
            card.saved_card = null;
        }
        card.saved_card = match.GiveCard(selected_card.ability,card.player);
        card.saved_card.overCard = card.ability.overCard;
        card.active = true;
        match.CardLog("Copyed",card,selected_card.player);
    }

}
