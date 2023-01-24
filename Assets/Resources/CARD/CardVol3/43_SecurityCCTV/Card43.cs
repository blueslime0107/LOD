using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/43경비용CCTV", order = 43)]
public class Card43 : CardAbility
{
    public override void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard)
    {
        if(!card.player.tag.Equals(player.tag)){return;}
        if(match.left_team.carddraw>0 || match.right_team.carddraw>0){
        player.AddHealth(-1);
        match.CardLog(card,player);}
    }
}
