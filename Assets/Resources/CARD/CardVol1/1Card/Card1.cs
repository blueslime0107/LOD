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

    public override void CardSelected(CardPack card, CardPack selected_card,BattleManager match)
    {
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

        selected_card.ability = match.null_card; // 능력 삭제 (봉인)
        
        
        

    }
}
