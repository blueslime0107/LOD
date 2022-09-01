using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/5숟가락 살인마", order = 5)]
public class Card5 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.card_activating = !card.card_activating;
    }

    public override void OnBattleStart(CardPack card, BattleCaculate battle)
    {
        if(!card.card_enable){return;}
        if(card.card_activating){
            if(battle.myChar.Equals(card.player)){
            battle.myChar.SetDice(1);
            battle.eneChar.SetDice(0);
            }
        }
        
    }
}
