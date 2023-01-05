using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14상호확증파괴", order = 14)]
public class Card14 : CardAbility
{

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(damage.origin == card){return;}
        card.diceLink.positionCount = 0;
        foreach(Player player in (attacker.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            player.DamagedByInt(1, card.player,damage,card);
            card.diceLink.positionCount += 2;
            card.diceLink.SetPosition(card.diceLink.positionCount-2,player.dice_Indi.gameObject.transform.position);
            card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
        }
        card.diceLink.gameObject.SetActive(true);
    }
}
