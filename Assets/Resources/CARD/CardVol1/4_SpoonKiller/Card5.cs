using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/4숟가락 살인마", order = 4)]
public class Card5 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.active = !card.active;
    }

    public override void OnClashStart(CardPack card, BattleCaculate battle)
    {
        if(card.active){
            if(battle.myChar.Equals(card.player)){
            battle.myChar.SetDice(1);
            battle.eneChar.SetDice(0);
            }
        }
        
    }
}
