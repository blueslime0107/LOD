using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/13현실부정", order = 13)]
public class Card13 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        card.active = false;
    }

    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player.dice < battle.eneChar.dice){
            card.player.SetDice((int)Random.Range(1,7));
            card.active = true;
            battle.bm.CardLog("RandomDice",card);
        }
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle)
    {
        if(!card.active){return;}
        card.player.AddHealth(battle.damage.value);
        battle.bm.CardLog("Health",card);
    }
}
