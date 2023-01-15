using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/46소스피란도", order = 50)]
public class Card502 : CardAbility
{
    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        damage.value -= 1;
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
