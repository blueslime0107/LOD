using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card81 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.diceLink.positionCount = 0;
        foreach(Player player in enemy.team.players){
            battle.bm.CardLog("POTATOOO",card,player); 
            if(player == enemy){
                player.NewDamagedByInt(battle.damage.value,card.player);
            }
            player.NewDamagedByInt(battle.damage.value*2,card.player);
            card.diceLink.positionCount += 2;
            card.diceLink.SetPosition(card.diceLink.positionCount-2,player.dice_Indi.gameObject.transform.position);
            card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
        }
        card.diceLink.gameObject.SetActive(true);
        battle.bm.DestroyCard(card,card.player);
    }

}
