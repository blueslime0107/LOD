using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/9비상식량", order = 40)]
public class Card40 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card,BattleManager match)
    {
        card.player.AddHealth(5,true);
        card.player.AddHealth(5);
        match.CardLog("Delicious",card);
    }

    public override void WhenCardDestroy(CardPack card, CardAbility card_abili)
    {
        card.player.AddHealth(-5,true);

        card.player.UpdateHp();
    }

}
