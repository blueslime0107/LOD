using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/8레이븐콜", order = 28)]
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
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.saved_card == null){return;}
        if(card.active){return;}
        
        if(card.count % 2 == 1){
            card.saved_card.player.cards.Remove(card.saved_card);
            card.saved_card = null;
            match.backColorEff.changeColor(255, 162, 0,255);
        }
        if(card.count == 1){
            card.active = true;
            card.saved_card.player.team.carddraw += 2;
        }
        card.count -= 1;
    }
}
