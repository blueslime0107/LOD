using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card36 : CardAbility
{
    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card, BattleManager match)
    {
        if(card.saved_card != null){return;}
        card.saved_card = selected_card;
        selected_card.cardStyle = card.ability.overCard;
        match.CardLog("ToEat!",card,selected_card.player);
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        if(card.saved_card == null || !card.active){return;}
        // Debug.Log("jsfkajsdlfj");
        card.count--;
        if(card.count <= 0){
            match.backColorEff.changeColor(255, 162, 0,255);
            match.CardLog("Eat!",card,card.saved_card.player);
            match.DestroyCard(card.saved_card,card.saved_card.player);
            card.saved_card = null;
            card.count = 3;
            card.sub_count++;
            if(card.sub_count >= 3){card.active = false;}
        }
    }
}
