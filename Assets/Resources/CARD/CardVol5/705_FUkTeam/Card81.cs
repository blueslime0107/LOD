using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card81 : CardAbility
{
    public override void OnClashWin(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.active = true;
        card.player.special_active = true;
        card.saved_player = enemy;
    }

    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        if(!card.active){return;}
        card.active = false;
        card.diceLink.positionCount = 0;
        foreach(Player player in card.saved_player.team.players){
            match.CardLog("POTATOOO",card,player); 
            if(player == card.saved_player){
                player.NewDamagedByInt(match.battleCaculate.damage.value,card.player);
            }
            else{player.NewDamagedByInt(match.battleCaculate.damage.value*2,card.player);}
            card.diceLink.positionCount += 2;
            card.diceLink.SetPosition(card.diceLink.positionCount-2,player.dice_Indi.gameObject.transform.position);
            card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
        }
        card.diceLink.gameObject.SetActive(true);
        match.DestroyCard(card,card.player);
    }

}
