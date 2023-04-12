using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Card85 : CardAbility
{
    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player){return;}
        if(card.active){return;}
        int allHealth = 0;
        foreach(Player player in match.players){
            allHealth += player.health;
            
        }
        int assBlood = (int)allHealth/match.players.Count;
        int remainBlood = 0;
        
        foreach(Player player in match.players){
            player.SetHealth(assBlood);
            if(assBlood > player.max_health){
                remainBlood += assBlood - player.max_health;
            }
            match.CardLog("Blood",card,player);
        }
        if(remainBlood > 0){
            remainBlood = (int)remainBlood/match.players.FindAll(x => x.health < x.max_health).Count;
            foreach(Player player in match.players.FindAll(x => x.health < x.max_health)){
            player.AddHealth(remainBlood);
        }
        }




        match.backColorEff.gameObject.SetActive(true);
        match.backColorEff.changeColor(255,0,0,230);
        card.active = true;
    }
}
