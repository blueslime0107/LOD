using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/24보험", order = 24)]
public class Card24 : CardAbility
{

    public override void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard)
    {
        if(!card.player.tag.Equals(player.tag)){return;}
        if(match.left_team.carddraw>0 || match.right_team.carddraw>0){
        match.CardLog(card,player);
        player.AddHealth(4);}
    }
}
