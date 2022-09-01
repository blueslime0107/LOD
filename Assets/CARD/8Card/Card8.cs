using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/8레이븐콜", order = 8)]
public class Card8 : CardAbility
{
    public override void BeforeCardDraw(CardPack card, BattleManager match, Player player)
    {
        if(!card.card_enable){return;}
        for(int i = 0; i<2;i++){
            match.game_cards.Insert(0, card.card_reg[i]);
        }
        match.card_give_count = 5;
        card.card_reg.Clear();
    }

    public override void ImmediCardDraw(BattleManager match, Player player)
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
        if(!card.card_enable){return;}
        card.card_reg.Add(card_abili);
    }
}
