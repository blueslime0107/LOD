using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card38 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        CardPack selected_card = card.player.cards[Random.Range(0,card.player.cards.Count)];
        if(!card.active){return;}
        if(selected_card.tained){return;}
        if(card.blocked){return;}
        if(selected_card == card){return;}
        if(card.saved_card != null){
            match.UnBlockCard(card.saved_card);
        }
        match.BlockCard(selected_card);
        card.saved_card = selected_card;
        selected_card.cardStyle = card.ability.overCard;
        match.CardLog("Lock",card,selected_card.player);
        
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card, BattleManager match)
    {
        if(card.saved_card == null){return;}
        card.active = true;
        if(selected_card == card.saved_card){
            match.CardLog("Debug Success!",card);

            card.player.AddHealth(5);
            card.player.AddDice(3);
            match.UnBlockCard(selected_card);
            card.saved_card = null;
        }
        else{
            match.CardLog("Debug Failed",card);
            match.DestroyCard(selected_card,card.player);
            card.saved_card = null;
        }
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        pre_count++;
        if(pre_count > 1){
            CardSelected(card, card.player.cards[Random.Range(0,card.player.cards.Count)], match);
            pre_count = 0;
        }
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(!card.active){return;}
        battle.bm.CardLog("GiveCard",card);
        Player player = (card.player.Equals(battle.myChar) ? battle.eneChar : battle.myChar);
        battle.bm.GiveCard(card.ability,player);
        card.count = 10;
        
    }

    public override void OnClashEnded(CardPack card, BattleCaculate battle)
    {
        if(card.count != 10){return;}
        battle.bm.CardLog("Fatal ERROR",card);
        battle.bm.DestroyCard(card,card.player);
    }
}
