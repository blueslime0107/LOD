using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/8살고자 하는 열망", order = 8)]
public class Card8 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        if(dead_player != card.player || card.count >= 2){return;}
        card.player.AddHealth(1);
        card.active = true;
        
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        if(!card.active){return;}
        player.SetDice(player.dice + 4);
    }

    public override void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match)
    {
        if(!card.active){return;}
        Active(card);
        damage.value = 0;
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.count += 1;
        if(card.count >= 2)
            card.active = false;
    }
}
