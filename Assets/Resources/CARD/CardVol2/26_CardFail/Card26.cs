using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card26 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }
    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        card.active = true;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card,BattleManager match)
    {
        if(!card.active){return;}
        if(selected_card.tained){return;}
        if(card.blocked){return;}
        if(selected_card == card){return;}

        if(card.saved_card != null){
            match.UnBlockCard(card.saved_card);
            card.saved_card.cardStyle = null;
        }
        match.BlockCard(selected_card);
        card.saved_card = selected_card;
        selected_card.cardStyle = card.ability.overCard;
        match.CardLog("Lock",card,selected_card.player);
        card.active = false;
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        List<CardPack> cardList = match.OpposeTeam(card.player.team).getAllCard();
        cardList.RemoveAll(x => x.blocked || x.tained);
        if(cardList.Count <= 0){return;}
        CardPack cardPack = cardList[Random.Range(0,cardList.Count)];
        CardSelected(card, cardPack,match);
    }
}
