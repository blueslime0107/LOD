using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card47 : CardAbility
{

    // public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    // {
    //     card.player.AddHealth(dead_player.cards.Count);
    // }

    public override void OnClashWin(CardPack card, BattleCaculate battle)
    {
        battle.damage.value += card.count;
        battle.bm.CardLog("PowerUp",card);
    }

    public override void WhenCardGet(CardPack card, BattleManager match, Player cardGetPlayer, CardPack getCard)
    {
        
        if(!cardGetPlayer.team.Equals(card.player.team)){return;}
        card.count = 0;
        Debug.Log(card.count);
        foreach(Player player in card.player.team.players ){
            if(player.Equals(card.player)){continue;}
            card.count += player.cards.Count;
        }
        Debug.Log(card.count);
        if(card.count >= card.player.cards.Count){
            Debug.Log(card.count);
            card.count = card.player.cards.Count;
        }

        Debug.Log(card.count);
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        match.SelectingPlayer(card);
    }

    public override void PlayerSelected(CardPack card, Player selected_player, BattleManager match)
    {
        match.GiveCard(linked_card[0],selected_player);
        match.CardLog("GiveCard",card);
        card.active = false;
        card.sub_count = 0;
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.sub_count += 1;
        if(card.sub_count >= 2){
            card.active = true;
            card.sub_count = 2;
        }

    }
}
