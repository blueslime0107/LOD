using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Card90 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {   
        if(card.active){return;}
        int manyCard = -1;
        foreach(Player pl in match.OpposeTeam(card.player.team).players){
            if(pl.cards.Count > manyCard){
                manyCard = pl.cards.Count;
                card.saved_player = pl;
            }
        }
        card.saved_card =  match.GiveCard(linked_card[0],card.saved_player,true);
        card.active = true;

    }

    public override void OnClashTargetSelected(CardPack card, Player target2, BattleManager match)
    {
        if(card.saved_card == null){return;}
        if(card.player != match.target1){return;}
        if(card.saved_card.active){
        match.CardLog("Harvest",card,card.saved_player);
        match.ChangeTarget2(card.saved_player);
        }
    }

    public override void OnClashStart(CardPack card, BattleCaculate battle, Player enemy)
    {

        if(card.saved_card == null){return;}
        if(!card.saved_card.active){return;}
        if(!enemy.cards.Contains(card.saved_card)){return;}
        battle.bm.GiveCard(linked_card[1],card.player,true,true);
        card.player.special_active = true;
        card.player.AddDice(99);
        battle.bm.DestroyCard(card,card.player);
    }

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player.cards.Contains(card.saved_card)){
            match.GiveCard(linked_card[1],card.player,true);
            match.DestroyCard(card,card.player);
            card.player.special_active = true;
        }
    }
}
