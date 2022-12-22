using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/13현실부정", order = 13)]
public class Card13 : CardAbility
{
    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        card.active = false;
    }

    public override void OnClashStart(CardPack card, BattleCaculate battle)
    {
        if(card.player.dice < battle.eneChar.dice){
            card.player.SetDice((int)Random.Range(1,7));
            card.active = true;
        }
    }

    public override void OnClashWin(CardPack card, BattleCaculate battle)
    {
        if(!card.active){return;}
        card.player.AddHealth(battle.damage.value);
    }
}
