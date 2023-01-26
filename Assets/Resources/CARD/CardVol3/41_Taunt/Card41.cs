using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/8레이븐콜", order = 28)]
public class Card41 : CardAbility
{
    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        match.SelectingPlayer(card);
    }

    public override void PlayerSelected(CardPack card, Player selected_player, BattleManager match)
    {
        if(selected_player.dice < card.player.dice ){return;}
        card.saved_player = selected_player;
        match.CardLog("AttackMe",card,selected_player);
        match.backColorEff.changeColor(196, 33, 255,255);
    }

    public override void OnClashTargetSelected(CardPack card, Player target2, BattleManager match)
    {
        if(card.saved_player == null || !card.active){return;}
        if(card.saved_player != match.target1){return;}
        match.CardLog("Taunted",card,card.saved_player);
        match.ChangeTarget2(card.player);
    }

    public override void WhoEverDamage(CardPack card, Damage damage, BattleManager match, Player attacker, Player defender)
    {
        if(card.saved_player == null || !card.active){return;}
        if(attacker != card.saved_player){return;}
        if(defender == card.player){return;}
        match.CardLog("Not Other!",card,attacker);
        damage.setDamage(0);

    }

    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.active = true;
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(card.saved_player == null){return;}
        card.active = false;        
        card.count++;
        if(card.count >= 2){
            card.active = true;
            card.saved_player = null;
            card.count = 0;
        }
    }


}
