using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/12도굴꾼 절친", order = 12)]
public class Card12 : CardAbility
{

    public override void OnDeathOurTeam(CardPack card)
    {
        card.card_activating = true;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        if(card.card_activating){
            card.saved_int = (int)Random.Range(1f,7f);
        }
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.card_activating){
            int dice = 0;
            dice = card.player.dice;
            card.player.SetDice(card.saved_int);
            card.saved_int = dice;
        }
        
    }
}
