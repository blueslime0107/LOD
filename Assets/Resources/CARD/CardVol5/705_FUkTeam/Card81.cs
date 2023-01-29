using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card81 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle)
    {
        battle.bm.CardLog("POTATOOO",card);
        foreach(Player player in battle.eneChar.team.players){
            if(player == battle.eneChar){
                player.NewDamagedByInt(battle.damage.value,card.player);
            }
            player.NewDamagedByInt(battle.damage.value*2,card.player);
        }
        battle.bm.DestroyCard(card,card.player);
    }

}
