using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/4숟가락 살인마", order = 4)]
public class Card4 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        card.active = !card.active;
        match.CardLog(card);
    }

    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.active){
            if(battle.myChar.Equals(card.player)){
            battle.myChar.SetDice(1);
            battle.eneChar.SetDice(0);
            battle.bm.CardLog(card,battle.eneChar);
            }
        }
        
    }
}
