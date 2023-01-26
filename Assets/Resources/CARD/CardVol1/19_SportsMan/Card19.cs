using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card19 : CardAbility
{
    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match,Player attacker,Player defender)
    {
        if(card.player.died){return;}
        if(!defender.tag.Equals(card.player.tag)){return;}
        if(defender.dice != 0){return;}
        match.GiveCard(linked_card[0],attacker);
        match.CardLog("illigal",card,attacker);
    }
}
