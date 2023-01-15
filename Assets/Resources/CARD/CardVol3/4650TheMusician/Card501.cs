using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card501 : CardAbility
{
    public override void OnDamaging(CardPack card, Player defender, Damage damage, BattleManager match)
    {
        damage.value += 1;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(card.active){return;}
        card.ability = linked_card[0];
        match.ui.CardReload(card.player.team.text);
    }

    public override void OnBattleEnd(CardPack card, Player player, BattleManager match)
    {
        card.active = true;
    }
}
