using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card14 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(card.card_battleActive){return;}
        Active(card);
        card.diceLink.positionCount = 0;
        foreach(Player player in attacker.team.players){
            match.CardLog(card,player);
            player.DamagedByInt(1, card.player,damage,card);
            card.diceLink.positionCount += 2;
            card.diceLink.SetPosition(card.diceLink.positionCount-2,player.dice_Indi.gameObject.transform.position);
            card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
            
        }
        card.diceLink.gameObject.SetActive(true);
        
    }
}
