using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/4평행세계", order = 1)]
public class Card4 : CardAbility
{
    
    public override void DiceApplyed(CardPack card, Player player)
    {
        player.SetDice(Mathf.Abs(player.dice-7));
        card.effect[0].SetActive(true);
        EffectPlayerSet(card.effect[0],player,player.dice_Indi.transform,0,0);
    }
}
