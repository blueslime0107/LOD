using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/17책장정리", order = 17)]
public class Card17 : CardAbility
{

    public override void OnDeath(CardPack card,BattleManager match)
    {
        if(card.player.gameObject.tag.Equals("PlayerTeam1")){
            foreach(Player player in match.left_players){
                if(!player.Equals(card.player)){
                    match.GiveCard(linked_card[0],player);
                }
            }
        }
        else{
            foreach(Player player in match.right_players){
                if(!player.Equals(card.player)){
                    match.GiveCard(linked_card[0],player);
                }
            }
        }   
    }

    public override void ImmediCardDraw(CardPack card, BattleManager match, Player player)
    {
        if(player.gameObject.tag == "PlayerTeam1"){
            match.card_left_draw += 1;
        }
        if(player.gameObject.tag == "PlayerTeam2"){
            match.card_right_draw += 1;
        }
    }
}
