using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/50페로체", order = 50)]
public class Card50 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player plaer, BattleManager match)
    {
        if(card.active){
            foreach(Player player in (card.player.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            player.SetDice(1);
            }
            card.active = false;
            return;
        }
        foreach(Player player in (card.player.tag.Equals("PlayerTeam1")) ? match.left_players : match.right_players){
            player.AddDice(1);
        }
    }

    public override void OnClashLose(CardPack card, BattleCaculate battle)
    {
        card.active = true;
        battle.bm.backColorEff.changeColor(255,255,255,200);
    }
}
