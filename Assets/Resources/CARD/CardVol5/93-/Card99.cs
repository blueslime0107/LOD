using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card99 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.cardStyle = card.ability.overCard;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        card.player.AddHealth(-10);
        card.active = true;
        card.effect[0].gameObject.SetActive(true);
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        if(!card.active){return;}
        if(Random.Range(0,2) == 1){
            card.player.AddDice(12);
            battle.damage.adDamage(12);
            card.active = false;
            card.player.special_active = true;
            card.effect[0].gameObject.SetActive(false);
        }
    }

    public override void AIgorithm(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        if(card.player.health > 15){
            CardActivate(card,match);
        }
    }
}
