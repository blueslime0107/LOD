using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card95 : CardAbility
{
    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        CardPack bloodcard = defender.cards.Find(x => x.card_id == 94);
        if(bloodcard == null){
        match.GiveCard(linked_card[0],defender,true);
        }
        else{
            bloodcard.count++;

        }
    }
}
