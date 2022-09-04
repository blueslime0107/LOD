using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/17.5책장", order = 61)]
public class Card175 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.player.gameObject.tag.Equals("PlayerTeam1")){
            foreach(Player player in match.left_players){
                if(player.cards.FindIndex(x => x.card_id.Equals(17)) > -1){
                    Debug.Log(player.cards.Count);
                    for(int i = 0;i<player.cards.Count;i++){
                        Debug.Log(player.cards);
                        if(player.cards[i].ability.card_id.Equals(17)){
                            Debug.Log("Equals(17)");
                            continue;
                        }
                        else{
                            Debug.Log("else");
                            match.GiveCardPack(player.cards[i],card.player);
                        }
                        
                    }   
                    player.cards.Clear();
               
                }
                if(player.cards.FindIndex(x => x.card_id.Equals(175)) > -1){
                    player.cards[player.cards.FindIndex(x => x.card_id.Equals(175))].ability = match.null_card;   
                    player.cards.RemoveAt(player.cards.FindIndex(x => x.card_id.Equals(175))) ;     
                }
            }
            
        }
        else{
            foreach(Player player in match.right_players){
                if(player.cards.FindIndex(x => x.card_id.Equals(17)) > -1){
                    Debug.Log(player.cards.Count);
                    for(int i = 0;i<player.cards.Count;i++){
                        Debug.Log(player.cards);
                        if(player.cards[i].ability.card_id.Equals(17)){
                            Debug.Log("Equals(17)");
                            continue;
                        }
                        else{
                            Debug.Log("else");
                            match.GiveCardPack(player.cards[i],card.player);
                        }
                        
                    }   
                    player.cards.Clear();
               
                }
                if(player.cards.FindIndex(x => x.card_id.Equals(175)) > -1){
                    player.cards[player.cards.FindIndex(x => x.card_id.Equals(175))].ability = match.null_card;   
                    player.cards.RemoveAt(player.cards.FindIndex(x => x.card_id.Equals(175))) ;     
                }
            }
        } 
    }
}
