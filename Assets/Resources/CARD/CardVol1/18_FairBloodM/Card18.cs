using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/14공정한 헌혈기", order = 18)]
public class Card18 : CardAbility
{

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        int allHealth = 0;
        foreach(Player player in match.players){
            allHealth += player.health;
            
        }
        allHealth = (int)allHealth/match.players.Count;
        foreach(Player player in match.players){
            player.health = allHealth;
            player.UpdateHp();
            
        }
        match.backColorEff.gameObject.SetActive(true);
        match.backColorEff.changeColor(255,0,0,230);
        card.active = true;
    }

    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match)
    {
        card.count = 0;
        foreach(Player player in match.players){
            card.count += player.health;
            
        }
        card.count = (int)card.count/match.players.Count;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.count = 0;
        foreach(Player playe in match.players){
            card.count += playe.health;
            
        }
        card.count = (int)card.count/match.players.Count;
    }
}
