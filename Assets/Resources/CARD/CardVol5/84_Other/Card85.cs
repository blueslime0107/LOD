using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Card85 : CardAbility
{
    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(card.player != dead_player){return;}
        List<Player> newPlayerList = new List<Player>(match.players);
        newPlayerList.Remove(card.player);
        int allHealth = 0;
        foreach(Player player in newPlayerList){
            allHealth += player.health;
            
        }
        allHealth = (int)allHealth/newPlayerList.Count;
        foreach(Player player in newPlayerList){
            player.SetHealth(allHealth);
            player.UpdateHp();
            match.CardLog("Blood",card,player);
        }
        match.backColorEff.gameObject.SetActive(true);
        match.backColorEff.changeColor(255,0,0,230);
        card.active = true;
    

    }
}
