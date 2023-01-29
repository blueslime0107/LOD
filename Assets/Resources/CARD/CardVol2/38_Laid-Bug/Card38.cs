using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card38 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        CardPack selected_card = card.player.cards[Random.Range(0,card.player.cards.Count)];
        if(selected_card == card){
            card.active = true;
            return;
        }
        if(selected_card.tained){return;}

        if(selected_card.ability.Equals(this)){ // 만약 선택한 카드가 의식실패 일때
            return; // 지나가기
        }

        card.saved_ability = selected_card.ability; // 봉인할 카드의 능력 저장한 후
        card.saved_card = selected_card; // 봉인 카드 저장
        //match.ui.CardReload();

        linked_card[0].illust = selected_card.ability.illust;
        linked_card[0].card_id = selected_card.ability.card_id;
        linked_card[0].xmlFile_path = selected_card.ability.xmlFile_path;
        selected_card.ability.WhenCardDestroy(selected_card,selected_card.ability);
        selected_card.ability = linked_card[0]; // 능력 삭제 (봉인)
        match.CardLog("ERROR",card,selected_card.player);
        
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
        match.CardLog("Debug",card);
        if(selected_card == card.saved_card){
            card.player.AddHealth(5);
            card.saved_card.ability = card.saved_ability;
            card.saved_card = null;
        }
        else{
            match.DestroyCard(selected_card,card.player);
            card.saved_card = null;
        }
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle)
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
