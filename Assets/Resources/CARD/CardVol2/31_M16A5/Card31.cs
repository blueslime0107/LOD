using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/31라이플", order = 31)]
public class Card31 : CardAbility
{
    public override void OnClashStart(CardPack card, BattleCaculate battle,Player enemy)
    {
        if(card.player.dice <= enemy.dice || card.active){return;}
        Dice newDice = dice; 
        newDice.dice_value = card.player.dice - enemy.dice;
        card.player.SetDice(enemy.dice);
        card.player.dice_Indi.put_subDice(newDice);
        card.active = true;

    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = false;
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        if(card.player.dice_Indi.dice_list[0].Equals(card.dice)){
            if(!card.player.farAtt)
                card.effect[0].SetActive(true);
        }
        
    }
}
