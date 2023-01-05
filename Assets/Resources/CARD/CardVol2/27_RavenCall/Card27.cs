using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/8레이븐콜", order = 27)]
public class Card27 : CardAbility
{
    public override void BeforeCardDraw(CardPack card, BattleManager match, Player player)
    {
        if(match.game_cards.Count<3) // 카드가 없을때 새로 다시 섞기
            match.game_cards = match.CardSuffle();
        for(int i = 0; i<2;i++){
            match.game_cards.Insert(0, card.card_reg[i]);
        }
        match.card_give_count = 5;
        card.card_reg.Clear();
    }

    public override void WhenCardGet(CardPack card,BattleManager match, Player player, CardPack getCard)
    {
        if(player.gameObject.tag == "PlayerTeam1"){
            match.card_left_draw += 1;
        }
        if(player.gameObject.tag == "PlayerTeam2"){
            match.card_right_draw += 1;
        }
    }

    public override void WhenCardDestroy(CardPack card, CardAbility card_abili)
    {
        card.card_reg.Add(card_abili);
    }
}
