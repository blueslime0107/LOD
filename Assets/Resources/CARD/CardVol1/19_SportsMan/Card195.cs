using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card195 : CardAbility
{
    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        Debug.Log(card.player.dice);
        damage.value = 0;
        match.CardLog(card);
    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){
            card.active = true;
            return;}
        match.DestroyCard(card,card.player);
    }

}
