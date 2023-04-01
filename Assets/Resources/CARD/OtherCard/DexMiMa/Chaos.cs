using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class Chaos : CardAbility
{
    public override void OnClashEnded(CardPack card, BattleCaculate battle)
    {
        if(battle.myChar != card.player){return;}
        if(card.active){return;}
        if(battle.eneChar.died){return;}

        card.active = true;
        List<DiceProperty> newDiceList = card.player.dice_Indi.dice_list;
        if(newDiceList.Count <= 0){return;}
        foreach(DiceProperty dice in newDiceList){
            battle.MakeNewEventBattle(card.player,battle.eneChar,dice);
        }

    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        card.active = false;
    }
}
