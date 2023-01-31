using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card36 : CardAbility
{
    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = false;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card, BattleManager match)
    {
        if(card.saved_card){return;}
        card.saved_card = selected_card;
        selected_card.overCard = card.ability.overCard;
        match.CardLog("ToEat!",card,selected_card.player);
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.saved_card == null){return;}
        if(card.active){return;}
        
        if(card.count % 2 == 1){
            match.DestroyCard(card.saved_card,card.saved_card.player);
            card.saved_card = null;
            match.backColorEff.changeColor(255, 162, 0,255);
            match.CardLog("Eat!",card,card.saved_card.player);
        }
        if(card.count == 1){
            card.active = true;
            match.AddCardPoint(card.saved_player.team,2);
            match.CardLog("I'm Full!",card);
        }
        card.count -= 1;
    }
}
