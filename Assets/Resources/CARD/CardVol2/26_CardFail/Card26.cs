using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card26 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingCard(card);
    }

    public override void CardSelected(CardPack card, CardPack selected_card,BattleManager match)
    {
        if(selected_card.tained){return;}
        try{
            card.saved_card.ability = card.saved_ability; // 봉인했던 카드 능력을 복구 시킴
            card.saved_card.overCard = null;
        }
        catch{
        }
            
        
        if(selected_card.ability.Equals(this)){ // 만약 선택한 카드가 의식실패 일때
            return; // 지나가기
        }

        card.saved_ability = selected_card.ability; // 봉인할 카드의 능력 저장한 후
        card.saved_card = selected_card; // 봉인 카드 저장
        selected_card.overCard = card.ability.overCard;
        //match.ui.CardReload();

        linked_card[0].illust = selected_card.ability.illust;
        linked_card[0].card_id = selected_card.ability.card_id;
        linked_card[0].xmlFile_path = selected_card.ability.xmlFile_path;
        selected_card.ability.WhenCardDestroy(selected_card,selected_card.ability);
        selected_card.ability = linked_card[0]; // 능력 삭제 (봉인)
        match.CardLog(card,selected_card.player);
        
        
        

    }
}
